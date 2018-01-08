﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.Utility.Common
{
    public class ValueConstant
    {
        // Value action in tracsaction
        public const string ACTION_INSERT = "I";
        public const string ACTION_UPDATE = "U";
        public const string ACTION_DELETE = "D";
        public const string ACTION_READ = "R";
        public const string ACTION_SEND = "S";
        public const string ACTION_CONFIRM = "C";

        public const string CORE_TAG = "core";
        public const string AUTHEN_TAG = "authen";
        public const string SET_PERMISSION = "/SetPermission";
        public const string UPDATE_PERMISSION = "/UpdatePermission";
        public const string REMOVE_PERMISSION = "/RemovePermission";
        public const string REGISTER = "/Register";
        public const string UPDATE_USER = "/UpdateUserInfo";
        public const string DELETE_USER = "/DeleteUser";
        public const string GET_ROLE = "/Role/GetAll";
        public const string SET_USER_ROLE = "/UserRole/SetUserRole";
        public const string GET_USER_LIST = "/GetListUser";
        public const string CHANGE_PASSWORD = "/ChangePassword";

        //Value path image Upload
        public const string ROOT_FOLDER = "Assets";
        public const string ROOT_FOLDER_VIDEO = "Videos";
        public const string IMAGE_PATH = "~\\Assets\\Images";
        public const string VIDEO_PATH = "~\\Assets\\Videos";
        public const string FOLDER_SEPARATOR = "\\";
        public const string IMAGE_POST_FULL_PATH = "~\\Assets\\images\\Posts";
        public const string LOGS_PATH = "~\\Assets\\Logs\\";
        public const string IMAGE_POST_PATH = "\\Posts\\";
        public const string IMAGE_DEFAULT = "News_default.png";
        public const string PATH_IMAGE_UPLOAD_DEFAULT = "~\\Assets\\images\\Posts\\News_default.png";
        public const string IMAGE_CATEGORY_PATH = "~\\Assets\\images\\Category";
        public const string IMAGE_FEATURE_PATH = "~\\Assets\\images\\Feature";
        public const string IMAGE_SLIDER_PATH = "~\\Assets\\images\\Slider";
        public const string IMAGE_ADVERTISE_PATH = "~\\Assets\\images\\Advertise";
        public const string NEW_FOLDER_NAME = "NewNode";
        public const string THUMBNAIL_VIDEO_FOLDER = "~\\Assets\\ThumbnailVideo\\";

        // Value default language
        public const byte LANG_VIETNAM = 1;
        public const byte WORK_STATE_ID_DRAFF = 1;
        public const byte WORK_STATE_ID_APPROVE = 2;
        public const byte WORK_STATE_ID_PUBLISH = 3;

        // Temp UserId
        public const int TEMP_USERID = 1;

        // Register info
        public const string PATH_IMAGE_DEFAULT = "/Assets/Avatar/user_default.jpg";
        public const string PATH_AVATAR = "/Assets/Avatar/";

        // Change password
        public const string INCORRECT_PASSWORD = "Incorrect password.";

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

        public enum ArticleStatus : byte
        {
            JustCreated = 1,
            Approved = 2,
            MoveToTrash = 3
        }
    }
    public enum UserStatus
    {
        Success = 1,
        IsUsed = 2,
    }
}