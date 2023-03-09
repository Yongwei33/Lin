using Dapper;
using Dapper.Contrib.Extensions;
using LinHong.Lib.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHong.Lib.Service
{
    public class RequisitionsService : BaseService
    {
        //查詢請購項目
        public List<SW_PRODUCT> getProduct(string keyword)
        {
            string sSql = @"SELECT * FROM SW_PRODUCT ";
            List<SW_PRODUCT> item = null;
            var dp = new DynamicParameters();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = $"%{keyword}%";
                sSql += " WHERE Part_No LIKE @keyword OR part_name LIKE @keyword ";
                dp.Add("@keyword", keyword);
            }

            using (var conn = this.GetConnection())
            {
                conn.Open();
                item = conn.Query<SW_PRODUCT>(sSql, dp).ToList();
            }
            return item;
        }

        //取得單筆請購項目
        public PRODUCT_GRIDITEM getOneProduct(string Part_No)
        {
            string sSql = @"SELECT * FROM SW_PRODUCT WHERE Part_No = @Part_No";
            PRODUCT_GRIDITEM items = null;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                items = conn.Query<PRODUCT_GRIDITEM>(sSql, new { Part_No }).FirstOrDefault();
            }
            return items;
        }

        //查詢供應商
        public List<SW_VENDOR1> getVendor(string keyword, string GroupName)
        {
            string sSql = @"SELECT * FROM V_SW_VENDOR WHERE owner_unit = @GroupName";
            List<SW_VENDOR1> item = null;
            var dp = new DynamicParameters();
            dp.Add("@GroupName", GroupName);
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = $"%{keyword}%";
                sSql += " AND ( vendor_name LIKE @keyword OR short_name LIKE @keyword )";
                dp.Add("@keyword", keyword);
            }
            using (var conn = this.GetConnection())
            {
                conn.Open();
                item = conn.Query<SW_VENDOR1>(sSql, dp).ToList();
            }
            return item;
        }

        //查詢請購單
        public List<SW_PR> getpurchaseProduct(string keyword, DateTime? start_date, DateTime? end_date, string buy_unit)
        {
            string sSql = @"SELECT * FROM SW_PR WHERE unpurchased_qty > 0 AND status = 1 ";
            List<SW_PR> item = null;
            var dp = new DynamicParameters();
            
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = $"%{keyword}%";
                sSql += " AND ( form_id LIKE @keyword OR part_no LIKE @keyword OR part_name LIKE @keyword OR part_type LIKE @keyword )";
                dp.Add("@keyword", keyword);
            }
            if (!string.IsNullOrEmpty(buy_unit))
            {
                sSql += " AND ( buy_unit = @buy_unit )";
                dp.Add("@buy_unit", buy_unit);
            }
            if (start_date.HasValue)
            {
                sSql += " AND ( require_date >= @start_date )";
                dp.Add("@start_date", start_date);
            }
            if (end_date.HasValue)
            {
                sSql += " AND ( require_date <= @end_date )";
                dp.Add("@end_date", end_date);
            }
            if (!end_date.HasValue && !start_date.HasValue)
            {
                string now = DateTime.Now.ToString("yyyyMMdd");
                sSql += " AND ( require_date >= @now )";
                dp.Add("@now", now);
            }

            using (var conn = this.GetConnection())
            {
                conn.Open();
                item = conn.Query<SW_PR>(sSql, dp).ToList();
            }
            return item;
        }

        //取得單筆請購單(GRID)
        public PR_GRIDITEM getOneRequisition(string form_id, string item_no)
        {
            string sSql = @"SELECT P.*, D.price, D.unit FROM SW_PR P LEFT OUTER JOIN SW_PRODUCT D ON P.part_no= D.Part_No WHERE P.form_id = @form_id AND P.item_no = @item_no 
";
            PR_GRIDITEM items = null;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                items = conn.Query<PR_GRIDITEM>(sSql, new { form_id, item_no }).FirstOrDefault();
            }
            return items;
        }

        //取得單筆請購單
        public SW_PR geteRequisitionQty(string form_id, string item_no)
        {
            string sSql = @"SELECT * FROM SW_PR WHERE form_id = @form_id AND item_no = @item_no 
";
            SW_PR items = null;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                items = conn.Query<SW_PR>(sSql, new { form_id, item_no }).FirstOrDefault();
            }
            return items;
        }

        //新增請購單
        public bool SaveRequisitionData(SW_PR formHead)
        {
            bool result = false;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                var tx = conn.BeginTransaction();
                try
                {
                    conn.Insert(formHead, tx);
                    result = true;
                    tx.Commit();

                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        //新增採購單
        public bool SavePurchaseData(SW_PO formHead)
        {
            bool result = false;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                var tx = conn.BeginTransaction();
                try
                {
                    conn.Insert(formHead, tx);
                    result = true;
                    tx.Commit();

                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        //更新請購單狀態
        public int UpdateRequisitions(string form_id, string status)
        {
            string cmdTxt = @"  UPDATE [dbo].[SW_PR]  
                                SET 
	                             [status] = @status 
                                WHERE 
	                            [form_id] = @form_id";
            int effRows = -1;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                try
                {
                    //注意為整個欄位一起更新,如POCO物件只塞要更新的欄位,其他會變空白,部份欄位請用UPDATE SQL
                    conn.Execute(cmdTxt, new
                    {
                        form_id,
                        status
                    });

                    effRows = 1;
                    //conn.Update<SampleForm>(form, tx);
                    //tx.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return effRows;
        }

        //更新請購單更新時間
        public int UpdateRequisitionsTime(string form_id, string time)
        {
            string cmdTxt = @"  UPDATE [dbo].[SW_PR]  
                                SET 
	                             [update_time] = @time 
                                WHERE 
	                            [form_id] = @form_id";
            int effRows = -1;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                try
                {
                    //注意為整個欄位一起更新,如POCO物件只塞要更新的欄位,其他會變空白,部份欄位請用UPDATE SQL
                    conn.Execute(cmdTxt, new
                    {
                        form_id,
                        time
                    });

                    effRows = 1;
                    //conn.Update<SampleForm>(form, tx);
                    //tx.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return effRows;
        }

        //更新請購單請購單位
        public int UpdateRequisitionsBuy_Unit(string form_id, string buy_unit)
        {
            string cmdTxt = @"  UPDATE [dbo].[SW_PR]  
                                SET 
	                             [buy_unit] = @buy_unit 
                                WHERE 
	                            [form_id] = @form_id";
            int effRows = -1;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                try
                {
                    //注意為整個欄位一起更新,如POCO物件只塞要更新的欄位,其他會變空白,部份欄位請用UPDATE SQL
                    conn.Execute(cmdTxt, new
                    {
                        form_id,
                        buy_unit
                    });

                    effRows = 1;
                    //conn.Update<SampleForm>(form, tx);
                    //tx.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return effRows;
        }

        //更新請購單採購量和未採購量
        public int UpdatePRpurchased_qty(string form_id, string item_no, Decimal? purchased_qty, Decimal? unpurchased_qty)
        {
            string cmdTxt = @"  UPDATE [dbo].[SW_PR]  
                                SET 
	                             [purchased_qty] = @purchased_qty, 
                                 [unpurchased_qty] = @unpurchased_qty
                                WHERE 
	                            [form_id] = @form_id AND [item_no] = @item_no";
            int effRows = -1;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                try
                {
                    //注意為整個欄位一起更新,如POCO物件只塞要更新的欄位,其他會變空白,部份欄位請用UPDATE SQL
                    conn.Execute(cmdTxt, new
                    {
                        form_id,
                        item_no,
                        purchased_qty,
                        unpurchased_qty
                    });

                    effRows = 1;
                    //conn.Update<SampleForm>(form, tx);
                    //tx.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return effRows;
        }

        //更新採購單狀態
        public int UpdatePOstatus(string form_id, string status)
        {
            string cmdTxt = @"  UPDATE [dbo].[SW_PO]  
                                SET 
	                             [status] = @status 
                                WHERE 
	                            [form_id] = @form_id";
            int effRows = -1;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                try
                {
                    //注意為整個欄位一起更新,如POCO物件只塞要更新的欄位,其他會變空白,部份欄位請用UPDATE SQL
                    conn.Execute(cmdTxt, new
                    {
                        form_id,
                        status
                    });

                    effRows = 1;
                    //conn.Update<SampleForm>(form, tx);
                    //tx.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return effRows;
        }

        //刪除請購單
        public int DeletePR(string form_id)
        {
            string cmdTxt = @"DELETE FROM [dbo].[SW_PR] WHERE [form_id] = @form_id";
            int effRows = -1;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                try
                {
                    //注意為整個欄位一起更新,如POCO物件只塞要更新的欄位,其他會變空白,部份欄位請用UPDATE SQL
                    conn.Execute(cmdTxt, new
                    {
                        form_id
                    });

                    effRows = 1;
                    //conn.Update<SampleForm>(form, tx);
                    //tx.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return effRows;
        }

        //刪除採購單
        public int DeletePO(string form_id)
        {
            string cmdTxt = @"DELETE FROM [dbo].[SW_PO] WHERE [form_id] = @form_id";
            int effRows = -1;
            using (var conn = this.GetConnection())
            {
                conn.Open();
                try
                {
                    //注意為整個欄位一起更新,如POCO物件只塞要更新的欄位,其他會變空白,部份欄位請用UPDATE SQL
                    conn.Execute(cmdTxt, new
                    {
                        form_id
                    });

                    effRows = 1;
                    //conn.Update<SampleForm>(form, tx);
                    //tx.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return effRows;
        }
    }
}
