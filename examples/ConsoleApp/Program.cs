﻿using System;
using Microsoft.Extensions.DependencyInjection;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace ConsoleApp
{
    internal class Program
    {
        private static IVkApi _api;

        private static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();

            _api = new VkApi(serviceCollection);

            _api.Authorize(new ApiAuthParams
            {
                Login = "ЛОГИН",
                Password = "ПАРОЛЬ",
                TwoFactorAuthorization = () =>
                {
                    Console.WriteLine(" > Введите код:");
                    return Console.ReadLine();
                }
            });

            var audios = _api.Audio.Get(new AudioGetParams {Count = 10});
            foreach (var audio in audios) Console.WriteLine($" > {audio.Artist} - {audio.Title}");

            Console.ReadLine();
        }
    }
}