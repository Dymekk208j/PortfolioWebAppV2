﻿using PortfolioWebAppV2.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioWebAppV2.Repository
{
    public class ImageRepository : IRepository<Image, int>
    {
        [Unity.Attributes.Dependency]
        public ApplicationDbContext Context { get; set; }

        public ImageRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Image> GetAll()
        {
            return Context.Images.ToList();
        }

        public Image Get(int id)
        {
            try
            {
                return Context.Images.First(a => a.ImageId == id);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool Add(Image entity)
        {
            Context.Images.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Remove(Image entity)
        {
            try
            {
                Image obj = Context.Images.First(a => a.ImageId == entity.ImageId);
                Context.Images.Remove(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return Context.SaveChanges() > 0;
        }

        public bool Remove(int imageId)
        {
            try
            {
                Image obj = Context.Images.First(a => a.ImageId == imageId);
                Context.Images.Remove(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return Context.SaveChanges() > 0;
        }

        public bool Update(Image entity)
        {
            try
            {
                Image image = Context.Images.Single(a => a.ImageId == entity.ImageId) ?? throw new InvalidOperationException();
                image.Favorite = entity.Favorite;
                image.FileName = entity.FileName;
                image.ImageType = entity.ImageType;

                return Context.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


    }
}