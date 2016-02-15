using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using Newtonsoft.Json;

namespace RedisSizeCalculator
{
    class Program
    {
        public static string redisHost = "127.0.0.1"; //Provide your Redis Ip Here
        public static string redisPort = "6379"; //Provide your Redis port Here
        public static string FilePathToStoreResult = "D:\\TestRedisKeySize.txt"; // Provide the name and location of txt file in which you want to see keynames and sizes 
        static void Main(string[] args)
        {
            findlen();
        }
        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
        static double ConvertBytesToKilobytes(long bytes)
        {
            return (bytes / 1024f);
        }

        static void findlen()
        {          
            using (var redisClient = new RedisClient(redisHost, Convert.ToInt16(redisPort)))
            {               
                double totalsize = 0;
                var keys = redisClient.GetAllKeys();
                foreach (string key in keys)
                {
                    try
                    { 
                        byte[] bytarr = redisClient.Get(key);
                        double kblen = ConvertBytesToKilobytes(bytarr.Length);
                        double mblen = ConvertBytesToMegabytes(bytarr.Length);
                        totalsize = totalsize + mblen;
                        Console.WriteLine("Key Name : " + key + " Key length in MB : " + mblen + " Key Length in Kb : " + kblen);
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePathToStoreResult, true))
                        {
                            file.WriteLine("Key Name : " + key + " Key length in MB : " + mblen + " Key Length in Kb : " + kblen);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            byte[][] bythsharr = redisClient.HGetAll(key);
                            double kblen = ConvertBytesToKilobytes(bythsharr.Length);
                            double mblen = ConvertBytesToMegabytes(bythsharr.Length);
                            Console.WriteLine("Hash Key Name : " + key + " Key length in MB : " + mblen + " Key Length in Kb : " + kblen);
                            using (System.IO.StreamWriter file =new System.IO.StreamWriter(@FilePathToStoreResult, true))
                            {
                                file.WriteLine("Hash Key Name : " + key + " Key length in MB : " + mblen + " Key Length in Kb : " + kblen);
                            }
                            totalsize = totalsize + mblen;
                        }
                        catch (Exception ex1)
                        {

                        }
                    }
                }                
            }
        }
    }
}
