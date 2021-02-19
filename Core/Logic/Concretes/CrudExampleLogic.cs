using Core.DataAccess;
using Core.Logic.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Logic.Concretes
{
    public class CrudExampleLogic : ICrudExampleLogic
    {
        private ModelContext ModelContext { get; }
        public CrudExampleLogic(ModelContext modelContext)
        {
            //this is using a process called Constructor Injection which is a style of Dependency Injection.
            //We INJECT our context into the contructor of this class and set the value of the injected one to the ModelContext.
            ModelContext = modelContext;
        }

        public List<CRUDExample> Get()
        {
            //make a list variable for holding the results
            var results = new List<CRUDExample>();

            //query database > CRUDExample table through ModelContext for a list of CRUDExamples **WHERE they are not deleted
            results = ModelContext.CRUDExamples.Where(x => !x.IsDeleted).ToList();

            //return the list
            return results;
        }

        public CRUDExample Get(int id)
        {
            //query database > CRUDExample table through ModelContext and use Single to give us the result with the matching Id
            // NOTE: Single is not going to return either 1 result or null.  It will return the 1 result or it will error your application.
            //        In this case I am kind of Okay with it erroring that application because people shouldn't be getting something that doesn't exist.
            return ModelContext.CRUDExamples.Single(x => x.Id == id);
        }

        //all in one create and update method.
        public CRUDExample Save(CRUDExample modelToSave)
        {
            //query database > CRUDExample table through ModelContext and use SingleOrDefault to give us either the 1 result with the matching ID or null
            var entity = ModelContext.CRUDExamples.SingleOrDefault(x => x.Id == modelToSave.Id);

            //if it didn't find anything with the same ID, we can kind of assume (in this case) that we are wanting to create one.  ELSE, we are updating the one it found
            if (entity == null)
            {
                //instantiate an instance of our CRUDExample class into our entity variable
                entity = new CRUDExample();
                entity.DateCreated = DateTimeOffset.Now;

                //entity.IsDeleted = false;
                //You may have wanted to add this line above as a sort of "When i'm constructing this object for the first time.  I want to make sure delete is set to false"
                //In this case, we don't have to because by default in C# (and most languages) if you instantiate a boolean and don't specifically set it to anything the default value is false;

                //making sure to add the entity to the model context (so it knows to create it and not just try to save it)  We will call ModelContext.SaveChanges() later.
                ModelContext.CRUDExamples.Add(entity);
            }

            //notice I set this property value OUTSIDE of the if statement above where we instantiated the CRUDExample class as an object into the entity variable.
            //by setting this value HERE instead it means that I will set the entity.Text to the modelToSave.Text whether I'm creating or whether I'm editing/updating.  
            entity.Text = modelToSave.Text;

            //save changes to our context
            ModelContext.SaveChanges();

            //return the  entity.  it may not always end up being used but it's a standard to do this (you don't have to do this though)
            return entity;
        }

        public void Delete(int id)
        {
            //Call our other Get method to give us a single result or null if it doesn't find anything with the same id
            var entity = Get(id);

            //set the deleted to true
            entity.IsDeleted = true;
            entity.DateDeleted = DateTimeOffset.Now;

            //save changes
            ModelContext.SaveChanges();
        }
    }
}