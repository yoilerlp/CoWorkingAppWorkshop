using CoWorkingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CoWorkingApp.Models.Enumerations;
namespace CoWorkingApp.Data
{

    public class DeskData
    {
        private JsonManager<Desk> jsonManager;

        public DeskData()
        {
            jsonManager = new JsonManager<Desk>();
        }

        public List<Desk> GetAllDesk()
        {
            return jsonManager.GetCollection();
        }

        public List<Desk> GetAvalibleDesk()
        {
            return jsonManager.GetCollection().Where(desk => desk.DeskStatus == DeskStatus.Active ).ToList();
        }
        public Desk FindDesk(string deskNumer, bool onlyActive = false)
        {
            var deskCollection = jsonManager.GetCollection();
            if(onlyActive) return deskCollection.FirstOrDefault(desk => desk.Number == deskNumer && desk.DeskStatus == DeskStatus.Active);
            return deskCollection.FirstOrDefault(desk => desk.Number == deskNumer);
        } 

        public bool CreateDesk(Desk newDesk)
        {
            try
            {
                var deskCollection = jsonManager.GetCollection();
                deskCollection.Add(newDesk);
                jsonManager.SaveCollection(deskCollection);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool EditDesk(Desk editDesk)
        {
            try
            {
                var deskCollection = jsonManager.GetCollection();
                var deskIndex = deskCollection.FindIndex(desk => desk.DeskId == editDesk.DeskId);
                deskCollection[deskIndex] = editDesk;
                jsonManager.SaveCollection(deskCollection);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDesk(Guid deskId)
        {

            try
            {
                var deskCollection = jsonManager.GetCollection();
                deskCollection.RemoveAt(deskCollection.FindIndex(u => u.DeskId == deskId));
                jsonManager.SaveCollection(deskCollection);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }



}