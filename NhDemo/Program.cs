using System;
using System.Linq;
using Microsoft.SqlServer.Server;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace NhDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //通过配置文件创建Nh Session的工厂
            ISessionFactory sessionFactory =
            new Configuration().Configure().BuildSessionFactory();

            //通过工厂创建Session对象
            var session = sessionFactory.OpenSession();//相当于EF的context
            var trans = session.BeginTransaction();


            #region 添加

            //for (int i = 0; i < 20; i++)
            //{
            //    Student student = new Student
            //    {
            //        SName = "yxp" + i.ToString(),
            //        SubTime = DateTime.Now
            //    };
            //    session.Save(student); //添加
            //}

            #endregion


            #region 删除

            //Student student1 = new Student();
            //student1.ID = 1;
            //session.Delete(student1);//删除一个实体

            #endregion

            #region 查询,支持linq

            var temp = from u in session.Query<Student>()
                       where u.ID < 10
                       select u;

            foreach (var stu in temp)
            {
                Console.WriteLine(stu.SName);
            }

            #endregion


            trans.Commit();
            session.Close();

            Console.Read();

        }
    }
}
