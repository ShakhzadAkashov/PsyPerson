import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule, registerLocaleData } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './pages/users/user/user.component';
import { LoginComponent } from './pages/users/user/login/login.component';
import { RegistrationComponent } from './pages/users/user/registration/registration.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './pages/home/home.component';
import { StatisticsComponent } from './pages/home/statistics/statistics.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { ru_RU } from 'ng-zorro-antd/i18n';
import ru from '@angular/common/locales/ru';
import { IconsProviderModule } from './pages/home/icons-provider.module';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { AuthInterceptor } from './services/auth/auth.interceptor';
import { StoreModule } from '@ngrx/store';
import { appReducers, metaReducers } from './store/reducers/app.reducers';
import { EffectsModule } from '@ngrx/effects';
import { UserEffects } from './store/effects/user.effects';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { environment } from 'src/environments/environment';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { TableModule } from 'primeng/table';
// import { SharedModule } from 'primeng/api';
import { ModalModule } from 'ngx-bootstrap/modal';
import { RoleEffects } from './store/effects/role.effects';
import { UserService } from './services/api/user.service';
import { RoleService } from './services/api/role.service';
import { TestService } from './services/api/test.service';
import { TestEffects } from './store/effects/test.effects';
import { AppFilesService } from './services/api/appFiles.serive';
import { UserTestService } from './services/api/userTest.service';
import { UserTestEffects } from './store/effects/userTest.effects';
import { SharedModule } from '../app/shared/shared.module';


registerLocaleData(ru);

@NgModule({
  declarations: [
    AppComponent,
    // UserComponent,
    // LoginComponent,
    // RegistrationComponent,
    HomeComponent,
    StatisticsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    ModalModule.forRoot(),
    IconsProviderModule,
    NzLayoutModule,
    NzMenuModule,
    TableModule,
    SharedModule,
    StoreModule.forRoot(appReducers,{metaReducers}),
    EffectsModule.forRoot([UserEffects,RoleEffects,TestEffects,UserTestEffects]),
    // StoreRouterConnectingModule.forRoot({ stateKey: 'router' }),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
  ],
  providers: [
    UserService,
    RoleService,
    TestService,
    AppFilesService,
    UserTestService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    { provide: NZ_I18N, useValue: ru_RU }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
