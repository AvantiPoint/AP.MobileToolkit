using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AP.MobileToolkit.Controls
{
    public class GravatarImageSource : ImageSource
    {
        public static readonly BindableProperty EmailProperty =
            BindableProperty.Create(nameof(Email), typeof(string), typeof(GravatarImageSource), null);

        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create(nameof(Size), typeof(int), typeof(GravatarImageSource), 20);

        public static readonly BindableProperty DefaultProperty =
            BindableProperty.Create(nameof(Default), typeof(DefaultGravatar), typeof(GravatarImageSource), DefaultGravatar.MysteryPerson);

        public string Email
        {
            get => (string)GetValue(EmailProperty);
            set => SetValue(EmailProperty, value);
        }

        public int Size
        {
            get => (int)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public DefaultGravatar Default
        {
            get => (DefaultGravatar)GetValue(DefaultProperty);
            set => SetValue(DefaultProperty, value);
        }

        public async Task<byte[]> GetGravatarAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(GetGravatarUri(Email));

                if (!response.IsSuccessStatusCode) return Array.Empty<byte>();

                return await response.Content.ReadAsByteArrayAsync();
            }
        }

        private const string RequestUri = "https://www.gravatar.com/avatar/{0}?s={1}&d={2}";

        private string GetGravatarUri(string email) => string.Format(RequestUri, GetMd5Hash(email), Size, DefaultGravatarName());

        private string DefaultGravatarName()
        {
            switch(Default)
            {
                case DefaultGravatar.FileNotFound:
                    return "404";
                case DefaultGravatar.MysteryPerson:
                    return "mp";
                default:
                    return $"{Default}".ToLower();
            }
        }

        private string GetMd5Hash(string str)
        {
            byte[] hash = null;
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            }

            var sBuilder = new StringBuilder();

            if (hash != null)
            {
                for (int i = 0; i < hash.Length; i++)
                {
                    sBuilder.Append(hash[i].ToString("x2"));
                }
            }

            return sBuilder.ToString();
        }
    }

    public enum DefaultGravatar
    {
        FileNotFound,
        MysteryPerson,
        Identicon,
        MonsterId,
        Wavatar,
        Retro,
        Robohash,
        Blank
    }
}
