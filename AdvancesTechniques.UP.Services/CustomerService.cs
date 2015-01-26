﻿using AdvancedTechniques.UP.Business.Model;
using AdvancedTechniques.UP.Persistence.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTechniques.UP.Services
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Add(Customer entity)
        {
            try
            {
                IRepository<Customer> customerRepository = new Repository<Customer>(this.unitOfWork);
                customerRepository.Create(entity);
                unitOfWork.Commit();

            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        public void Edit(Customer entity)
        {
            IRepository<Customer> customerRepository = new Repository<Customer>(this.unitOfWork);

            customerRepository.Update(entity);
        }

        public void Delete(Customer entity)
        {
            IRepository<Customer> customerRepository = new Repository<Customer>(this.unitOfWork);

            customerRepository.Remove(entity);
        }

        public Customer GetById(int entityId) 
        {
            IRepository<Customer> customerRepository = new Repository<Customer>(this.unitOfWork);

            Expression<Func<Customer, bool>> criteria = x => x.CustomerId == entityId;

            var searchResult = customerRepository.Find(criteria);

            return searchResult.FirstOrDefault();
        }

        IEnumerable<Customer> IService<Customer>.GetAll()
        {
            IRepository<Customer> customerRepository = new Repository<Customer>(this.unitOfWork);

            return customerRepository.GetAll();
        }

        public IList<Customer> GetCustomerByName(string customerName)
        {
            IRepository<Customer> customerRepository = new Repository<Customer>(this.unitOfWork);

            Expression<Func<Customer, bool>> criteria = x => x.LastName.Contains(customerName);

            return customerRepository.Find(criteria).ToList();
        }
    }
}
