﻿using _23._1News.Data;
using _23._1News.Models.Db;
using _23._1News.Models.View_Models;
using _23._1News.Models.ViewModels;
using _23._1News.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace _23._1News.Services.Implement
{
    public class SubscriptionService: ISubscriptionService
    {

        private readonly ApplicationDbContext _db;
       
        public SubscriptionService(ApplicationDbContext db)
        {
            _db = db;
           
        }

        
        
        public List<Subscription> GetAllSubs()
        {
            return _db.Subscriptions.ToList();
        }

        public void CreateSubs(Subscription newSub) 
        {
            newSub.SubscriptionType = _db.SubscriptionTypes.Where(t => t.Id == newSub.SubscriptionTypeId).FirstOrDefault()!;
            newSub.Created = DateTime.Now;
            newSub.Price = newSub.SubscriptionType.Price;
            _db.Subscriptions.Add(newSub);
            _db.SaveChanges();
        }

        public bool UpdateSubs(Subscription newSubs) 
        {
            try
            {
                _db.Subscriptions.Update(newSubs);
                _db.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public Subscription GetSubsById(int id)
        {
            return _db.Subscriptions.Find(id);
        }

        public List<Subscription> GetSubsByUserId(string id)
        {
            var subscriptions = _db.Subscriptions.Where(Subscription => Subscription.UserId== id)
                            .OrderByDescending(a => a.Created).ToList();

            return subscriptions;
        }

        public bool DeleteSubs(int id)
        {
            try
            {
                var sub = this.GetSubsById(id);
                if (sub == null)
                {
                    return false;
                }
                _db.Subscriptions.Remove(sub);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int GetActiveSubscribersCount()
        {
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            return _db.Subscriptions.Count(s => s.Created >= thirtyDaysAgo);
        }

        object ISubscriptionService.GetSubsByUserId(string? userId)
        {
            throw new NotImplementedException();
        }
    }

}
