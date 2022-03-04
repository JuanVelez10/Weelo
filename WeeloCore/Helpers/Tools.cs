using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WeeloCore.Entities;
using WeeloInfrastructure.Repositories;
using static WeeloCore.Helpers.EnumType;

namespace WeeloCore.Helpers
{
    //Class for general methods used by the logic layer
    public class Tools
    {
        MessageRepository messageRepository;

        public Tools()
        {
            messageRepository = new MessageRepository();
        }

        //Method to get success or error messages from database
        public string GetMessage(int Code, MessageType messageType)
        {
            return messageRepository.GetAll().Where(x => x.Code == Code && x.MessageType == (int)messageType).Select(x => x.Message1).FirstOrDefault();
        }

        //Method to save an image in firebase storage
        public async Task<string> UpLoadImage(Stream stream, string fileName, IConfiguration config)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(config.GetSection("Storage")["ApiKey"]));
            var a = await auth.SignInWithEmailAndPasswordAsync(config.GetSection("Storage")["AuthEmail"], config.GetSection("Storage")["AuthPassword"]);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                 config.GetSection("Storage")["Bucket"],
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("Property")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);

            try
            {
                return await task;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        //Method to validate image
        public bool IsImage(string image)
        {
            if (Regex.IsMatch(image.ToLower(), @"^.*\.(jpg|gif|png|jpeg)$")) return true;
            return false;
        }

    }
}
