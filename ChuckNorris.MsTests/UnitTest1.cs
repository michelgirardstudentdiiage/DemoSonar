using ChuckNorris.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Transactions;

namespace ChuckNorris.MsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // la classe à tester
            Call call = new Call();
            var url = "https://api.chucknorris.io/jokes/random";

            var result = call.GetDataFromApi(url);
            Assert.IsNotNull(result);

            JokeChuckNorris joke
                = JsonConvert.DeserializeObject<JokeChuckNorris>(result);

            Assert.IsNotNull(joke);
            Assert.IsNotNull(joke.value);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // la classe à tester
            Call call = new Call();
            var url = "https://api.chucknorris.io/jokes/categories";

            var results = call.GetChuckNorrisJokeCategories(url);
            Assert.IsNotNull(results);

            Assert.AreEqual(16, results.Count);
        }



        [TestMethod]
        public void TestMethodDB()
        {
            JokeChuckNorris jokeChuckNorris = new JokeChuckNorris();
            jokeChuckNorris.id = "J41MNnYJTXGku-GVCdvqsg";
            jokeChuckNorris.value = "Chuck Norris simply walks into Mordor";
            string connectionString
             // = "Data Source=.;Initial Catalog=Db2022ChuckNorris;User ID=Db2022ChuckNorrisUser;Password=Db2022ChuckNorrisUser";
             = @"Data Source=.;Initial Catalog=Db2022ChuckNorris;User ID=Db2022ChuckNorrisUser;Password=Db2022ChuckNorrisUser";
            using (var txscope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                Db db = new Db(connectionString);
                var result = db.AddJoke(jokeChuckNorris);
                Assert.AreEqual("Added", result);
                result = db.AddJoke(jokeChuckNorris);
                Assert.AreEqual("Already in the " +
                    "database", result);
                txscope.Dispose();
            }
        }
    }
}