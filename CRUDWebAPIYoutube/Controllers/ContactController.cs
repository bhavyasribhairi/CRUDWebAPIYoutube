using AutoMapper;
using CRUDWebAPIYoutube.Data;
using CRUDWebAPIYoutube.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWebAPIYoutube.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    

    public class ContactController : Controller
    {

        public ContactController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        private readonly ApplicationDbContext dbContext;

        private readonly IMapper mapper;

        [HttpGet]
        [Route("showContacts")]
        public List<Contact> GetContacts()
        {
            List<Contact> contacts = dbContext.Contacts.ToList();
            return contacts;
        }


        [HttpPost]
        [Route("insert")]
        public bool InsertContact(Contact contact)
        {
            if (contact != null  && ModelState.IsValid)
            {
                Contact existingcontact = dbContext.Contacts.FirstOrDefault(id => id.Id == contact.Id);
                if (existingcontact != null)
                {
                    var existingContact= mapper.Map<Contact>(existingcontact);
                    //existingcontact.FullName = contact.FullName;
                    //existingcontact.Address = contact.Address;
                    //existingcontact.Email = contact.Email;
                    //existingcontact.Phone = contact.Phone;
                    dbContext.Contacts.Update(existingContact);
                    dbContext.SaveChanges();
                    return true;

                }
                contact.Id = Guid.NewGuid();
                dbContext.Contacts.Add(contact);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpDelete]
        [Route("remove")]
        public bool DeleteContact(string contactName)
        {
            if(!string.IsNullOrEmpty(contactName))
            {
              Contact existingContact= dbContext.Contacts.FirstOrDefault(name=> name.FullName== contactName);
                if (existingContact != null)
                {
                    dbContext.Contacts.Remove(existingContact);
                    dbContext.SaveChanges() ;
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
