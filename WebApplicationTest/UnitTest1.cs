using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Models;
using WebApplication.Repository;

namespace WebApplicationTest
{
    
    [TestClass]
    public class WebApplicationTest
    {
        private CommentsRepository _commentsRepository;
         
        [TestInitialize]
        public void TestInitialization()
        {
            _commentsRepository = new CommentsRepository();
            

        }
        [TestMethod]
        public void GetCommentById()
        {
            
            Comment expectedObj = new Comment();
            expectedObj.ArticleId = 1;
            var comments = _commentsRepository.FindById(1);
            Assert.IsTrue(comments.Contains(expectedObj));
        }
    }
}