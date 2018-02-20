import { Component, OnInit } from '@angular/core';
import { ContextService } from '../../services/context.service';
import { CalendarService } from '../../services/calendar.service';
import { TransactionService } from '../../services/transaction.service';
import { Transaction } from '../../entities/transaction';
import { Account } from '../../entities/account';
import { AccountService } from '../../services/account.service';
import { TransactionType } from '../../entities/transaction-type';
import { TransactionTypeService } from '../../services/transaction-type.service';
import { Category } from '../../entities/category';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'mint-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  year: number;
  years: number[];
  month: number;
  months: any[];
  account: number;
  totalIncome: number = 0;
  totalIncomePercent: number = 100;
  totalExpense: number = 0;
  totalExpensePercent: number = 0;
  netValue: number = 0;
  netValuePercent: number = 0;

  accounts: Account[];
  types: TransactionType[];
  categories: Category[];
  subCategories: Category[];
  transactions: Transaction[];

  transaction: Transaction = {
    id: 0,
    date: "",
    description: "",
    type: 0,
    category: 0,
    subCategory: 0,
    amount: 0
  };
  
  constructor(private context: ContextService,
    private calendar: CalendarService,
    private typeService: TransactionTypeService,
    private categoryService: CategoryService,
    private accountService: AccountService,
    private transactionService: TransactionService) {
  }

  ngOnInit() {
    this.context.setContext(1, 1);
    this.years = this.calendar.getYears();
    this.months = this.calendar.getMonths(0);
    this.year = this.calendar.getCurrentYear();
    this.month = this.calendar.getCurrentMonth();

    this.loadAccounts();
    this.loadTypes();
    this.loadCategories();
    this.loadTransactions();
  }

  loadAccounts() {
    this.accountService.getAccounts().subscribe(data => {
      this.accounts = data;
      this.account = this.context.getContext().account;
    }, error => {
      console.log(error);
    });
  }

  loadTypes() {
    this.typeService.getTypes().subscribe(data => {
      this.types = data;
    }, error => {
      console.log(error);
    });
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe(data => {
      this.categories = data;
    }, error => {
      console.log(error);
    });
  }

  loadSubCategories(category: number) {
    this.categoryService.getSubCategories(category).subscribe(data => {
      this.subCategories = data;
    }, error => {
      console.log(error);
    });
  }

  loadTransactions() {
    this.transactionService.getTransactions(this.month, this.year, 100).subscribe(data => {
      this.transactions = data;
      this.calculateTotal();
    }, error => {
      console.log(error);
    });
  }

  changeAccount() {
    this.context.changeAccount(this.account);
  }

  createTransaction() {
    let transaction = new Transaction();
    transaction.id = 0;
    transaction.date = this.transaction.date;
    transaction.type = parseInt(this.transaction.type.toString());
    transaction.category = parseInt(this.transaction.category.toString());
    transaction.subCategory = parseInt(this.transaction.subCategory.toString());
    transaction.amount = parseFloat(this.transaction.amount.toString());
    transaction.description = this.transaction.description;

    this.transactionService.createTransaction(transaction).subscribe(data => {      
      this.loadTransactions();
    }, error => {
      console.log(error);
    });
  }

  deleteTransaction(id: number) {
    this.transactionService.deleteTransaction(id).subscribe(data => {
      this.loadTransactions();
    }, error => {
      console.log(error);
    });
  }

  showSummary(): boolean {
    if (typeof this.transactions != 'undefined')
      return this.transactions.length > 0;
    else
      return false;
  }

  calculateTotal() {
    this.totalIncome = 0;
    this.totalExpense = 0;
    this.netValue = 0;
    for (let i of this.transactions) {
      if (i.type.name == "Income") {
        this.totalIncome += i.amount;
      }
      if (i.type.name == "Expense") {
        this.totalExpense += i.amount;
      }
    }
    this.netValue = this.totalIncome - this.totalExpense;
    this.totalExpensePercent = (this.totalExpense / this.totalIncome) * 100;
    this.netValuePercent = (this.netValue / this.totalIncome) * 100;
  }
}
