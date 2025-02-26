using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ConstantValues
    {
        public enum msgType
        {
            LOGIN,
            GENERATE_TOKEN,
            REFRESH_TOKEN,
            GENERATE_OTP,
            ONBOARD_PARTNER,
            UPDATE_PARTNER,
            ONBOARD_ISSUER,
            UPDATE_ISSUER,
            ADD_MEMBER,
            RESEND_BANK_OTP,
            GET_MEMBER,
            VERIFY_MEMBER,
            MIN_KYC_MEMBER,
            FULL_BIO_KYC_MEMBER,
            SAVE_MODULE,
            ASSIGN_CARD,
            FETCH_CARD,
            LOCK_CARD,
            UNLOCK_CARD,
            BLOCK_CARD,
            REPLACE_CARD,
            PIN_SET,
            ACCOUNT_BALANCE,
            DEBIT_ACCOUNT,
            LOAD,
            REVERSAL,
            GET_BALANCE
        }

        public enum channel
        {
            WEB,
            APP,
            ONLINE,
            INSTORE,
            API,
            OFFLINE
        }
        public enum msg
        {
            SUCCESS,
            FAIL,
            UNKNOWN_EXCEPTION,
            ISSUER_NOT_FOUND,
            VALIDATION_FAILURE,
            SYSTEM_FAILURE,
            ACCOUNT_ALREADY_EXISTS,
            ACCOUNT_DOESNT_EXIST,
            DATABASE_EXCEPTION,
            DATABASE_LOCK_TIMEOUT,
            UNHANDLED_TXN_TYPE,
            INSUFFICIENT_BALANCE,
            DATABASE_INCONSISTENT,
            MEMBER_NOT_FOUND,
            DUPLICATE_MOBILE_NUMBER,
            MEMBER_STATUS_MISMATCH,
            MEMBER_DATA_MISMATCH,
            MEMBER_ALREADY_ASSIGNED_CARD,
            CARD_KIT_NOT_FOUND,
            UNALLOCATED_CARD_KIT,
            INTERNAL_SERVER_ERROR,
            CARD_ASSIGNED,
            CARD_LOCKED,
            CARD_ACTIVATED,
            CARD_BLOCKED,
            CARD_NOT_ASSIGNED,
            CARD_ALREADY_ALLOCATED,
            UNSUPPORTED_OPERATION,
            FAILURE_AT_BANK_END,
            UNPROVISIONED,
            MERCHANT_FAILURE
        }

        public const string _publicKey = "MIIBITANBgkqhkiG9w0BAQEFAAOCAQ4AMIIBCQKCAQB4sfz2ten846QKPWHbsqgg\r\n9IXD6lY3noQBRZDmI62OG6ZzbtkblkjoeL7fYPmGAS275YLIScsDauENl1kOFKGu\r\nE7ANFAwrC8RX8svBQF59fXN7ETbohCEtM0Y1NigCG85FYaDgSpDJs7XqTfHU8skv\r\nNZq57q2vRBIMVK+fAxuEbC6lt330JfQsbNAUAjcIxe41y9j4lPr5/EPRIUT2vSzU\r\nDWmb0mno0US2ZHdx779EBYjo67NcU/e6CCj4XuWFXvIzpssRf478WhEn7lDHkQgs\r\njMHtoSLkwlHboVFreGgBsFEIbtphv9JbhvXdd3mCzbMinhTFWMSShgytOm1eQcZT\r\nAgMBAAE=";

        public const string _privateKey = "MIIEogIBAAKCAQB4sfz2ten846QKPWHbsqgg9IXD6lY3noQBRZDmI62OG6Zzbtkb\r\nlkjoeL7fYPmGAS275YLIScsDauENl1kOFKGuE7ANFAwrC8RX8svBQF59fXN7ETbo\r\nhCEtM0Y1NigCG85FYaDgSpDJs7XqTfHU8skvNZq57q2vRBIMVK+fAxuEbC6lt330\r\nJfQsbNAUAjcIxe41y9j4lPr5/EPRIUT2vSzUDWmb0mno0US2ZHdx779EBYjo67Nc\r\nU/e6CCj4XuWFXvIzpssRf478WhEn7lDHkQgsjMHtoSLkwlHboVFreGgBsFEIbtph\r\nv9JbhvXdd3mCzbMinhTFWMSShgytOm1eQcZTAgMBAAECggEAWhwAkaz1WeIW2bJE\r\nOkNjNN96cu+kmPfW9CAc80VV0RAhsHLk0qzcF/v1/U4oXPzKSUsr3h8B27ZS+dOy\r\nIEoU/6MhQFJkV0r8tvDFYK2PHj+oFQygIu5q2pU6aIXbTt+1m6Us9+eTu5NZUOoK\r\nkAMHYyCxOPnSYFUA6IXdbGlkhPM5RGriD4rEUenf4nHmDjYl4RPZRLJM3fQ4D+lh\r\n95e9T0bdNU4qoeaCvXhp02NN900BX8v6t9rda9jKQxbD51CBMOVFkHBqoYQXxPeA\r\nBgs56FGICnKHLruD4FACjbgYNOANEZPFESitXqdhXJVQz4sMuv6Xf0sFFODjdULR\r\nHYaUCQKBgQDRiOc//kjelURKSFuFyXIAh47JDS0uLZM7+ef1LaFvkAY9v57nhqRt\r\nStS1Wy0f3eIo7Sm1PSP0lhClb+DI/G/0A956QEj0WX1Vw11MVrU2Rdkbsxyu/d82\r\nOdo9j/kn4bjAzLxlBlI4vmLcPmUqWGqZC/o09D7BSpnaMYos5/hSLwKBgQCTdb9V\r\nr4TyElSn/Zurx38lFkpGzm1iiKY24a+JQo/cRALrnAR2fZstYhiPP/pSSRmv1MLl\r\nJmeeBZKY2BKA5WSAqXUEyqGm39U/p2+oX95rpzf/d4aNgEWpSTMEEYUhuLHwxv3K\r\nqURY/vtnqxoLoCFgDSxzHwJ6HL4Vz4VAr2w5HQKBgQCtIAWZ3TtOFVzAV3qvVttH\r\nzrlOKwHqhqOBSaG69UHOkNxnRp0/xQK0fsCzO6tUChukHxh39BVyME9+saqzxcem\r\ngU/gCJ9+rTYel3XN7lJ5jkqtVVdcyswT3McSWJPPvPPO8Rq0Cf4DfmLmgPNLgpRI\r\n8hmAiVIUmNP38Tiuk9eb5wKBgFgZikX0RpamEwQHBiG8YB9VUO062al8AOpbLhfw\r\nt6iezavcm+H7K2IS8J9tVu4glIMzt3lW16NWqv48Ydm2s7QXCj3hnnDn3C1aqOBM\r\n3sMstc0gqTgTQgthG2S73vGFunIjQW/6b0ZImQWX+Uim1Cnh1QO2rDEuyPaY9IDw\r\nDdWVAoGAJQ+A3klZOyx+R92K6oZFocCgnvIAFMF1TGCVXpyqSJL8VXKDYTs+ea4H\r\nwGb6QGBSGYexhUHX7ZndQH3TqmuafLL1Zs/CQiOAhair4rZ+M1i2yjh1P54pcx7R\r\n32ESTuxuve3FHE441PiWJvp5cVgZjRyTXrz3Tkqfe9NLf9i18Ww=\r\n";


        public const string url = "http://localhost:5127";
        public const string liveURL = "https://api.xyz.com";
        public const string clientid = "16652c18-9d72-49e6-8908-5aeec60dfa92";
        public const string clientsecret = "2Wv2cjFIuv52Wy57Le3ePbt2pWDN7o";
        public const string issuerId = "d77981f2-6c38-483c-8946-f5285d69286e";

        public const string UrlGetToken = "/auth/token/api/create";
        public const string UrlAddMemnber = "/api/v2/member/add";
        public const string UrlVerifyMember = "/api/v2/member/verify";
        public const string UrlResendOtp = "/api/v2/member/add/resend";
        public const string UrlGetMember = "/api/v2/member/get";
        public const string UrlAssignCard = "/api/v2/card/prepaid/assign";
        public const string UrlLockCard = "/api/v2/card/prepaid/lock";
        public const string UrlUnlockCard = "/api/v2/card/prepaid/unlock";
        public const string UrlBlockCard = "/api/v2/card/prepaid/block";
        public const string UrlReplaceCard = "/api/v2/card/prepaid/replace";
        public const string UrlPinset = "/api/v2/card/prepaid/reset";
        public const string UrlFetchCardbyIssuer = "/api/v2/card/prepaid/fetch/by-issuer";
        public const string UrlFetchCardbyMember = "/api/v2/card/prepaid/fetch/by-member";
        public const string UrlAccountBalanceCheck = "";
        public const string UrlDebitAccount = "";
        public const string UrlDebitReversal = "";
        public const string UrlLoadCard = "/api/v2/cards/transaction/load";
        public const string UrlUnloadCard = "/api/v2/cards/transaction/unLoad";
        public const string UrlCardInventory = "/api/v1/card/prepaid/get/inventory";
        public const string UrlCardBalance = "/api/v2/cards/transaction/balance";
    }
}
