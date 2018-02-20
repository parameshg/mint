// modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { AlertModule } from 'ngx-bootstrap/alert';
// components
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { ErrorComponent } from './components/error/error.component';
import { MissingComponent } from './components/missing/missing.component';
import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';
import { ProfileComponent } from './components/profile/profile.component';
import { SettingsComponent } from './components/settings/settings.component';
// services
import { CalendarService } from './services/calendar.service';
import { ContextService } from './services/context.service';
import { AccountService } from './services/account.service';
import { UserService } from './services/user.service';
import { CategoryService } from './services/category.service';
import { TagService } from './services/tag.service';
import { TransactionService } from './services/transaction.service';
import { TransactionTypeService } from './services/transaction-type.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ErrorComponent,
    MissingComponent,
    LoginComponent,
    LogoutComponent,
    ProfileComponent,
    SettingsComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    ModalModule.forRoot(),
    BsDropdownModule.forRoot(),
    AlertModule.forRoot(),
    BsDatepickerModule.forRoot()
  ],
  providers: [
    CalendarService,
    ContextService,
    UserService,
    AccountService,
    TransactionTypeService,
    CategoryService,
    TagService,
    TransactionService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
