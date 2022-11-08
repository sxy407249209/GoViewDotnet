using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoViewWtm.ViewModel.GoViewApi
{
    public class GoViewDataReturn
    {
        public int code { get; set; }
        public int count { get; set; }
        public string  msg { get; set; }
        public object data { get; set; }
    }
    /// <summary>
    /// 获取文件上传oss信息
    /// </summary>
    public class OssInfo
    {
        public string BucketName { get; set; }
        public string bucketURL { get; set; }
    }

    /// <summary>
    /// 用户信息返回数据
    /// </summary>
    public class userData
    {
        public userinfo userinfo { get; set; }

        public token token { get; set; }
    }
    /// <summary>
    /// 用户信息
    /// </summary>
    public class userinfo
    {
        public string id { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public string depId { get; set; }
        public string posId { get; set; }
        public string depName { get; set; }
        public string posName { get; set; }

    }

    public class token 
    {
        public string tokenName { get; set; }
        public string tokenValue { get; set; }
        public bool isLogin { get; set; }
        public string loginId { get; set; }
        public string loginType { get; set; }
        public int tokenTimeout { get; set; }
        public int sessionTimeout { get; set; }
        public int tokenSessionTimeout { get; set; }
        public int tokenActivityTimeout { get; set; }
        public string loginDevice { get; set; }
        public string tag { get; set; }

    }


    public class GoviewFileInfo 
    {
        public string id { get; set; }
        public string fileName { get; set; }
        public string bucketName { get; set; }
        public long fileSize { get; set; }
        public string fileSuffix { get; set; }
        public string createUserId { get; set; }
        public string createUserName { get; set; }
        public DateTime createTime { get; set; }
        public string updateUserId { get; set; }
        public string updateUserName { get; set; }
        public string updateTime { get; set; }

    }
}
