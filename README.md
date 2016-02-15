# RedisSizeCalculator
This is small .net c# based application which can find the total size occupied by various keys in Redis, Please feel free to contribute.
In Program.cs file of application please configure 
 public static string redisHost = "127.0.0.1"; //Provide your Redis Ip Here
        public static string redisPort = "6379"; //Provide your Redis port Here
        public static string FilePathToStoreResult = "D:\\TestRedisKeySize.txt"; // Provide the name and location of txt file in which you want to see keynames and sizes 
        according to your settings and requirements.
