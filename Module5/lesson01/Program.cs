using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace lesson01
{
    class Program
    {
        private static async Task GetBy(string link)
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync($@"{link}");

                var content = await result.Content.ReadAsStringAsync();

                Console.WriteLine($"Link: {link}");
                Console.WriteLine($"Status Code: {result.StatusCode}");
                Console.WriteLine($"Content: {content}");
                Console.WriteLine();

            }
        }

        static void Main(string[] args)
        {
            GetListUsers().GetAwaiter().GetResult();
            GetSingleUser().GetAwaiter().GetResult();
            GetSingleUserNotFound().GetAwaiter().GetResult();
            GetListResource().GetAwaiter().GetResult();
            GetSingleResource().GetAwaiter().GetResult();
            GetSingleResourseNotFound().GetAwaiter().GetResult();
            PostCreate().GetAwaiter().GetResult();
            PutUpdate().GetAwaiter().GetResult();
            PatchUpdate().GetAwaiter().GetResult();
            Delete().GetAwaiter().GetResult();
            Register().GetAwaiter().GetResult();
            Login().GetAwaiter().GetResult();
            DelayedResponse().GetAwaiter().GetResult();
        }

        private static async Task GetListUsers()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync(@"https://reqres.in/api/users?page=2");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<UserConfig>(content);

                    Console.WriteLine($"Link: https://reqres.in/api/users?page=2");
                    Console.WriteLine($"Status Code: {result.StatusCode}");
                    Console.WriteLine($"Content:");
                    foreach (var user in users.User)
                    {
                        Console.WriteLine($"Id: {user.Id}");
                        Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                        Console.WriteLine($"email: {user.Email}");
                    }
                    Console.WriteLine();
                }
            }
        }

        private static async Task GetSingleUser()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync(@"https://reqres.in/api/users/2");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var user = JsonConvert.DeserializeObject<UserConfig1>(content);

                    Console.WriteLine($"Link: https://reqres.in/api/users/2");
                    Console.WriteLine($"Status Code: {result.StatusCode}");
                    Console.WriteLine($"Content:");
                    Console.WriteLine($"Id: {user.User.Id}");
                    Console.WriteLine($"Name: {user.User.FirstName} {user.User.LastName}");
                    Console.WriteLine($"email: {user.User.Email}");
                    Console.WriteLine();
                }
            }
        }

        private static async Task GetSingleUserNotFound()
        {
            await GetBy("https://reqres.in/api/users/23");
        }

        private static async Task GetListResource()
        {
            await GetBy("https://reqres.in/api/unknown");
        }

        private static async Task GetSingleResource()
        {
            await GetBy("https://reqres.in/api/unknown/2");
        }

        private static async Task GetSingleResourseNotFound()
        {
            await GetBy("https://reqres.in/api/unknown/23");
        }

        private static async Task PostCreate()
        {
            using (var httpClient = new HttpClient())
            {
                var payload = new
                {
                    name = "morpheus",
                    job = "leader"
                };

                var httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var httpMessage = new HttpRequestMessage();
                httpMessage.Content = httpContent;
                httpMessage.RequestUri = new Uri(@"https://reqres.in/api/users");
                httpMessage.Method = HttpMethod.Post;

                var result = await httpClient.SendAsync(httpMessage);

                var content = await result.Content.ReadAsStringAsync();

                Console.WriteLine($"Link: https://reqres.in/api/users");
                Console.WriteLine($"Status Code: {result.StatusCode}");
                Console.WriteLine($"Content: {content}");
                Console.WriteLine();

            }
        }

        private static async Task PutUpdate()
        {
            using (var httpClient = new HttpClient())
            {
                var payload = new
                {
                    name = "morpheus",
                    job = "zion resident"
                };

                var httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var httpMessage = new HttpRequestMessage();
                httpMessage.Content = httpContent;
                httpMessage.RequestUri = new Uri(@"https://reqres.in/api/users/2");
                httpMessage.Method = HttpMethod.Put;

                var result = await httpClient.SendAsync(httpMessage);

                var content = await result.Content.ReadAsStringAsync();

                Console.WriteLine($"Link: https://reqres.in/api/users/2");
                Console.WriteLine($"Status Code: {result.StatusCode}");
                Console.WriteLine($"Content: {content}");
                Console.WriteLine();
            }
        }

        private static async Task PatchUpdate()
        {
            using (var httpClient = new HttpClient())
            {
                var payload = new
                {
                    name = "morpheus",
                    job = "zion resident"
                };

                var httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var httpMessage = new HttpRequestMessage();
                httpMessage.Content = httpContent;
                httpMessage.RequestUri = new Uri(@"https://reqres.in/api/users/2");
                httpMessage.Method = HttpMethod.Post;

                var result = await httpClient.SendAsync(httpMessage);

                var content = await result.Content.ReadAsStringAsync();

                Console.WriteLine($"Link: https://reqres.in/api/users/2");
                Console.WriteLine($"Status Code: {result.StatusCode}");
                Console.WriteLine($"Content: {content}");
                Console.WriteLine();
            }
        }

        private static async Task Delete()
        {
            using(var httpClient = new HttpClient())
            {
                var result = await httpClient.DeleteAsync(@"https://reqres.in/api/users/2"); 
                var content = await result.Content.ReadAsStringAsync();

                Console.WriteLine($"Link: https://reqres.in/api/users/2");
                Console.WriteLine($"Status Code: {result.StatusCode}");
                Console.WriteLine($"Content: {content}");
                Console.WriteLine();
            }
        }

        private static async Task Register()
        {
            using (var httpClient = new HttpClient())
            {
                var payload = new
                {
                    email = "eve.holt@reqres.in",
                    password = "pistol"
                };

                var httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var result = await httpClient.PostAsync(@"https://reqres.in/api/register", httpContent);
                var content = await result.Content.ReadAsStringAsync();

                Console.WriteLine($"Link: https://reqres.in/api/register");
                Console.WriteLine($"Status Code: {result.StatusCode}");
                Console.WriteLine($"Content: {content}");
                Console.WriteLine();
            }
        }

        private static async Task Login()
        {
            using(var httpClient = new HttpClient())
            {
                var payload = new
                {
                    email = "eve.holt@reqres.in",
                    password = "cityslicka"
                };

                var httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var result = await httpClient.PostAsync(@"https://reqres.in/api/login", httpContent);
                var content = await result.Content.ReadAsStringAsync();

                Console.WriteLine($"Link: https://reqres.in/api/login");
                Console.WriteLine($"Status Code: {result.StatusCode}");
                Console.WriteLine($"Content: {content}");
                Console.WriteLine();
            }
        }

        private static async Task DelayedResponse()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync(@"https://reqres.in/api/users?delay=3");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<UserConfig>(content);

                    Console.WriteLine($"Link: https://reqres.in/api/users?delay=3");
                    Console.WriteLine($"Status Code: {result.StatusCode}");
                    Console.WriteLine($"Content:");
                    foreach (var user in users.User)
                    {
                        Console.WriteLine($"Id: {user.Id}");
                        Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                        Console.WriteLine($"email: {user.Email}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
