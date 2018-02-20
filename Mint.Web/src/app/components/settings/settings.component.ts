import { Component, OnInit } from '@angular/core';
import { Account } from '../../entities/account';
import { TransactionType } from '../../entities/transaction-type';
import { AccountService } from '../../services/account.service';
import { TransactionTypeService } from '../../services/transaction-type.service';
import { Category } from '../../entities/category';
import { CategoryService } from '../../services/category.service';
import { Tag } from '../../entities/tag';
import { TagService } from '../../services/tag.service';

@Component({
  selector: 'mint-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  accounts: Account[];
  types: TransactionType[];
  categories: Category[];
  tags: Tag[];

  constructor(private accountService: AccountService,
    private typeService: TransactionTypeService,
    private categoryService: CategoryService,
    private tagService: TagService) {
  }

  ngOnInit() {

    this.loadAccounts();

    this.loadTypes();

    this.loadCategories();

    this.loadTags();
  }

  loadAccounts() {
    this.accountService.getAccounts().subscribe(data => {
      this.accounts = data;
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
      this.categories = data;
    }, error => {
      console.log(error);
    });
  }

  loadTags() {
    this.tagService.getTags().subscribe(data => {
      this.tags = data;
    }, error => {
      console.log(error);
    });
  }

  changeCategory(id: number) {
    if (id === 0) {
      this.loadCategories();
    } else {
      this.loadSubCategories(id);
    }
  }

  createAccount(name: string, description: string) {
    let account = new Account();
    account.name = name;
    account.description = description;

    this.accountService.createAccount(account).subscribe(data => {
      if (data) {
        this.loadAccounts();
      }
    }, error => {
      console.log(error);
    });
  }

  createCategory(name: string, description: string) {
    let category = new Category();
    category.name = name;
    category.description = description;

    this.categoryService.createCategory(category).subscribe(data => {
      if (data) {
        this.loadCategories();
      }
    }, error => {
      console.log(error);
    });
  }

  createTag(name: string, description: string) {
    let tag = new Tag();
    tag.name = name;
    tag.description = description;

    this.tagService.createTag(tag).subscribe(data => {
      if (data) {
        this.loadTags();
      }
    }, error => {
      console.log(error);
    });
  }

  deleteAccount(id: number) {
    this.accountService.deleteAccount(id).subscribe(data => {
      if (data) {
        this.loadAccounts();
      }
    }, error => {
      console.log(error);
    });
  }

  deleteCategory(id: number) {
    this.categoryService.deleteCategory(id).subscribe(data => {
      if (data) {
        this.loadCategories();
      }
    }, error => {
      console.log(error);
    });
  }

  deleteTag(id: number) {
    this.tagService.deleteTag(id).subscribe(data => {
      if (data) {
        this.loadTags();
      }
    }, error => {
      console.log(error);
    });
  }
}
