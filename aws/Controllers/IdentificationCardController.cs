using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using aws.Contexts;
using aws.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aws.Controllers
{
    public class IdentificationCardController : Controller
    {

        private readonly IdentificationCardContext _context;

        public IdentificationCardController(IdentificationCardContext context)
        {
            _context = context;
        }

        // GET: IdentificationCard
        public ActionResult Index()
        {
            return View();
        }

        // GET: IdentificationCard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IdentificationCard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IdentificationCard/Create
        [HttpPost]
        public ActionResult Create(IdentificationCard idCard, HttpPostedFile file)
        {
            var accessKey = "ak1a4smywog35ynp2f6s";
            var secretKey = "cjfBZE1eyejGcwUkZHfB/WAt1tUR7ntiBQquJbRJ";
            RegionEndpoint bucketRegion = RegionEndpoint.SAEast1;

            var s3client = new AmazonS3Client(accessKey, secretKey, bucketRegion);

            var fileTransferUtility = new TransferUtility(s3client);

            string _FileName = Path.GetFileName(file.FileName);
            string _path = Path.Combine(DateTime.Now.ToString(), _FileName);

            try
            {
                _context.IdentificationCards.Add(idCard);

                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = "",
                    FilePath = _path,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    PartSize = 6291456,
                    Key = _FileName,
                    CannedACL = S3CannedACL.PublicRead
                };
                fileTransferUtility.UploadAsync(fileTransferUtilityRequest).GetAwaiter().GetResult();

                return RedirectToAction("Index");
            }
            catch (AmazonS3Exception s3exception)
            {
                if(s3exception.ErrorCode != null && 
                    (s3exception.Equals("InvalidAccessKeyId") || s3exception.Equals("InvalidSecurity")))
                {
                    return View("Invalid AWS Credentials.");
                }
                else
                {
                    return View("Error occurred: " + s3exception.Message);
                }
            }
        }

        // GET: IdentificationCard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IdentificationCard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IdentificationCard idCard)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: IdentificationCard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IdentificationCard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
