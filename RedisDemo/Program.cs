using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RedisClient("127.0.0.1", 6379);

            //最后一个参数为我们排序的依据
            const string setId = "Set1";
            var s = client.AddItemToSortedSet(setId, "百度", 400);

            client.AddItemToSortedSet(setId, "谷歌", 300);
            client.AddItemToSortedSet(setId, "阿里", 200);
            client.AddItemToSortedSet(setId, "新浪", 100);
            client.AddItemToSortedSet(setId, "人人", 500);

            //升序获取最一个值:"新浪"
            var list = client.GetRangeFromSortedSet(setId, 0, 0);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            //降序获取最一个值:"人人"
            list = client.GetRangeFromSortedSetDesc(setId, 0, 0);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            //上面是添加到set，还可以添加到队列和栈

            const string QueueId = "Queue1";
            client.EnqueueItemOnList(QueueId, "a1");
            client.EnqueueItemOnList(QueueId, "a2");
            client.EnqueueItemOnList(QueueId, "a3");
            var item1 = client.DequeueItemFromList(QueueId);
            Console.WriteLine(item1);


            const string StackId = "Stack1";
            client.PushItemToList(StackId,"b1");
            client.PushItemToList(StackId, "b2");
            client.PushItemToList(StackId, "b3");

            var item2 = client.PopItemFromList(StackId);
            Console.WriteLine(item2);

            Console.Read();
        }
    }
}
