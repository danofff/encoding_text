using Encoding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Encoding.Controllers
{

    public class EncodeController : ApiController
    {
        private Dictionary<char, char> encoder = new Dictionary<char, char>()
        {
            { 'a','한' },
            { 'b','리' },
            { 'c','열' },
            { 'd','점' },
            { 'e','또' },
            { 'f','을' },
            { 'g','촌' },
            { 'h','소' },
            { 'i','시' },
            { 'j','산' },
            { 'k','공' },
            { 'l','기' },
            { 'm','건' },
            { 'n','간' },
            { 'o','궁' },
            { 'p','심' },
            { 'q','과' },
            { 'r','수' },
            { 's','지' },
            { 't','문' },
            { 'u','두' },
            { 'v','근' },
            { 'w','방' },
            { 'x','무' },
            { 'y','일' },
            { 'z','왈' },
            { ' ',' ' },
            { '.','.' },
            { ',',',' },
            { '\'','\'' },
            { '?','?' },
            { '!','!' },
            { '-','-' },
        };
        private string token  = "144B26ED-BC42-47DC-8F65-2B7ADACF1E10";
        [HttpPost]
        [Route("api/encode/signup")]
        // POST api/values
        public void SignUp(string name,string email,string password)
        {
            User registerUser = new User()
            {
                Name = name,
                Email = email,
                Password = password,
                Token = Guid.NewGuid()
            };
            MyContext _context = new MyContext();
            _context.Users.Add(registerUser);
            _context.SaveChanges();

            //var cookie = new CookieHeaderValue("token", registerUser.Token.ToString());
            //cookie.Domain = Request.RequestUri.Host;
            //cookie.Path = "/";
            //var response = Request.CreateResponse(HttpStatusCode.OK);
            //response.Headers.AddCookies(new CookieHeaderValue[] { cookie });         
        }

        [HttpPost]
        [Route("api/encode/encode")]
        public string Encode(string text)
        {
            var user = getUser(token, text);
            if (user != null)
            {
                return EncodeText(text.ToLower());
            }
            return "no such user";
        }

        [HttpPost]
        [Route("api/encode/decode")]
        public string Decode(string text)
        {
            var user = getUser(token, text);
            if (user != null)
            {
                return DecodeText(text);
            }
            return "no such user";
        }


        private User getUser(string token, string text)
        {
            MyContext _context = new MyContext();
            var user = _context.Users.SingleOrDefault(w => w.Token.ToString() == token);
            if (user != null)
            {
                user.Queries.Add(text);
                _context.SaveChanges();
            }
            return user;
        }

        private string EncodeText(string text)
        {
            char[] chars = text.ToCharArray();
            char[] encodedArray=new char[text.Length];
            try
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    encodedArray[i] = encoder[chars[i]];
                }
                string encodeText = new string(encodedArray);
                return encodeText;
            }
            catch (Exception ex)
            {

                string encodeText = "you're text couldn't be decoded";
                return encodeText;
            } 
        }

        private string DecodeText(string text)
        {
            char[] chars = text.ToCharArray();
            char[] decodedArray = new char[text.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                decodedArray[i] = encoder.FirstOrDefault(x=>x.Value==chars[i]).Key;
            }
            string encodeText = new string(decodedArray);

            return encodeText;
        }
    }
}
