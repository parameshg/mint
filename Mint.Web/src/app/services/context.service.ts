import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { Context } from '../entities/context';

@Injectable()
export class ContextService {

  private context: Context;

  constructor() {
    this.context = new Context();
    this.context.user = 0;
    this.context.account = 0;
  }

  getHeaders() {
    return {
      headers: new HttpHeaders({
        'X-Mint-User': window.sessionStorage.getItem("user"),
        'X-Mint-Account': window.sessionStorage.getItem("account")
      })
    };
  }

  getContext(): Context {
    this.context.user = parseInt(window.sessionStorage.getItem("user"));
    this.context.account = parseInt(window.sessionStorage.getItem("account"));
    return this.context;
  }

  setContext(user: number, account: number) {
    window.sessionStorage.setItem("user", user.toString());
    window.sessionStorage.setItem("account", account.toString());
  }

  changeUser(user: number) {
    window.sessionStorage.setItem("user", user.toString());
  }

  changeAccount(account: number) {
    window.sessionStorage.setItem("account", account.toString());
  }
}
