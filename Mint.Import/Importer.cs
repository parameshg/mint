using Mint.Business.Services.Interfaces;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Import
{
    public class Importer : IProcessor
    {
        private ICategoryService Category { get; }

        private ISubCategoryService SubCategory { get; }

        private ITransactionTypeService TransactionType { get; }

        private ITransactionService Transaction { get; }

        private List<string> Sheets { get; set; }

        public Importer(ICategoryService category,
            ISubCategoryService subCategory,
            ITransactionTypeService transactionType,
            ITransactionService transaction)
        {
            Category = category;
            SubCategory = subCategory;
            TransactionType = transactionType;
            Transaction = transaction;

            Sheets = new List<string>(new string[] {
                "01-Jan",
                "02-Feb",
                "03-Mar",
                "04-Apr",
                "05-May",
                "06-Jun",
                "07-Jul",
                "08-Aug",
                "09-Sep",
                "10-Oct",
                "11-Nov",
                "12-Dec"
            });
        }

        public async Task Execute(string filename)
        {
            var workbook = new XSSFWorkbook(filename);

            foreach (var x in Sheets)
            {
                var sheet = workbook.GetSheet(x);

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);

                    var cindex0 = row?.GetCell(0)?.ToString();
                    var cindex1 = row?.GetCell(1)?.ToString();
                    var cindex2 = row?.GetCell(2)?.ToString();
                    var cindex3 = row?.GetCell(3)?.ToString();
                    var cindex4 = row?.GetCell(4)?.ToString();
                    var cindex5 = row?.GetCell(5)?.ToString();
                    var cindex6 = row?.GetCell(6)?.ToString();

                    if (string.IsNullOrEmpty(cindex0) ||
                        string.IsNullOrEmpty(cindex1) ||
                        string.IsNullOrEmpty(cindex2) ||
                        string.IsNullOrEmpty(cindex3) ||
                        string.IsNullOrEmpty(cindex4) ||
                        string.IsNullOrEmpty(cindex5) ||
                        string.IsNullOrEmpty(cindex6))
                        continue;

                    var sn = int.Parse(cindex0);
                    var date = DateTime.Parse(cindex1);
                    var description = cindex2;
                    var amount = double.Parse(cindex6);

                    var category = await Category.GetCategoryByName(cindex3);
                    var categoryId = category != null ? category.ID : await Category.CreateCategory(cindex3, cindex3);

                    var subCategory = await SubCategory.GetSubCategoryByName(cindex3, cindex4);
                    var subCategoryId = subCategory != null ? subCategory.ID : await SubCategory.CreateSubCategory(categoryId, cindex4, cindex4);

                    var transactionType = await TransactionType.GetTransactionTypeByName(cindex5);
                    var transactionTypeId = transactionType != null ? transactionType.ID : 0;

                    await Transaction.CreateTransaction(date, description, categoryId, subCategoryId, transactionTypeId, amount);
                }
            }
        }
    }
}