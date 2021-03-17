using InteractiveContactList.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InteractiveContactList.Services
{
    class ContactsService
    {
        const string Url = "https://localhost:44346/api/contacts/"; // обращайте внимание на конечный слеш
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        // получаем все контакты
        public async Task<IEnumerable<Contact>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<Contact>>(result, options);
        }

        // добавляем однин контакт
        public async Task<Contact> Add(Contact contact)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
            new StringContent(
            JsonSerializer.Serialize(contact),
            Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Contact>(
            await response.Content.ReadAsStringAsync(), options);
        }
        // обновляем контакт
        public async Task<Contact> Update(Contact contact)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(Url,
            new StringContent(
            JsonSerializer.Serialize(contact),
            Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Contact>(
            await response.Content.ReadAsStringAsync(), options);
        }
        // удаляем контакт
        public async Task<Contact> Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Contact>(
            await response.Content.ReadAsStringAsync(), options);
        }
    }
}
