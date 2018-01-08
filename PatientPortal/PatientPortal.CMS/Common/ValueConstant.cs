﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.CMS.Common
{
    public class ValueConstant
    {
        //Value path image Upload
        public const string ROOT_FOLDER = "Assets";
        public const string ROOT_FOLDER_VIDEO = "Videos";
        public const string IMAGE_PATH = "~\\Assets\\images";
        public const string IMAGE_POST_FULL_PATH = "~\\Assets\\images\\Posts";
        public const string VIDEO_PATH = "~\\Assets\\Videos";
        public const string LOGS_PATH = "~\\Assets\\Logs\\";
        public const string IMAGE_POST_PATH = "\\Posts\\";
        public const string IMAGE_DEFAULT = "News_default.png";
        public const string FOLDER_SEPARATOR = "\\";
        public const string PATH_IMAGE_DEFAULT = "~\\Assets\\images\\Resources\\no-image.jpg";
        public const string IMAGE_CATEGORY_PATH = "~\\Assets\\images\\Category";
        public const string IMAGE_GALLERY_PATH = "~\\Assets\\images\\Gallery";
        public const string IMAGE_FEATURE_PATH = "~\\Assets\\images\\Feature";
        public const string IMAGE_SLIDER_PATH = "~\\Assets\\images\\Slider";
        public const string IMAGE_ADVERTISE_PATH = "~\\Assets\\images\\Advertise";
        public const string NEW_FOLDER_NAME = "NewNode";
        public const string THUMBNAIL_VIDEO_FOLDER = "~\\Assets\\ThumbnailVideo\\";
        public const string PATH_IMAGE_USER_DEFAULT = "/Assets/Avatar/user_default.jpg";
        public const string PATH_AVATAR = "/Assets/Avatar/";

        // Value default language
        public const byte LANG_VIETNAM = 1;
        public const byte WORK_STATE_ID_DRAFF = 1;
        public const byte WORK_STATE_ID_APPROVE = 2;
        public const byte WORK_STATE_ID_PUBLISH = 3;

        // Temp UserId
        //public const int TEMP_USERID = 1;

        public enum MEDIA_TYPE
        {
            IMAGE,
            VIDEO
        }

        public enum POST_STATUS
        {
            NEW = 1,
            DRAFT = 2,
            TRASH = 3
        }
        public enum UserStatus
        {
            Success = 1,
            IsUsed = 2,
        }
        // Change password
        public const string INCORRECT_PASSWORD = "Incorrect password.";
    }
}