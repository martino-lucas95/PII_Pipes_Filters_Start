using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU;


namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture NoFilterPicture = provider.GetPicture(@"luke.jpg");
            FilterGreyscale greyscale = new FilterGreyscale();
            FilterNegative filterNegative = new FilterNegative();

            PipeNull pipeNull = new PipeNull();
            PipeSerial pipeSerialNegative = new PipeSerial(filterNegative, pipeNull);
            PipeSerial pipeSerialGrey = new PipeSerial(greyscale, pipeNull);

            provider.SavePicture(pipeNull.Send(NoFilterPicture), @"LukeNull.jpg");
            provider.SavePicture(pipeSerialNegative.Send(NoFilterPicture), @"LukeNegativeFilter.jpg");
            provider.SavePicture(pipeSerialGrey.Send(NoFilterPicture), @"LukeGreyFilter.jpg");

            PictureProvider provider2 = new PictureProvider();
            IPicture NoFilterBeer = provider.GetPicture(@"beer.jpg");
            FilterGreyscale greyscale2 = new FilterGreyscale();
            FilterNegative filterNegative2 = new FilterNegative();

            PipeNull pipeNull2 = new PipeNull();
            PipeSerial pipeSerialNegative2 = new PipeSerial(filterNegative2, new PipeNull());
            PipeSerial pipeSerialGrey2 = new PipeSerial(greyscale2, pipeNull2);

            provider2.SavePicture(pipeNull2.Send(NoFilterBeer), @"BeerNull.jpg");
            provider2.SavePicture(pipeSerialNegative2.Send(NoFilterBeer), @"BeerNegativeFilter.jpg");
            provider2.SavePicture(pipeSerialGrey2.Send(NoFilterBeer), @"BeerGreyFilter.jpg");

            var twitter = new TwitterImage();
            Console.WriteLine(twitter.PublishToTwitter("I am Bill! ",@"bill2.jpg"));
            var twitterDirectMessage = new TwitterMessage();
            Console.WriteLine(twitterDirectMessage.SendMessage("Hola!", "249011461"));
        }
    }
}
