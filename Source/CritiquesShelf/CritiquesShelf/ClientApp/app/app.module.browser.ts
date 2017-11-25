import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './components/app/app.component';
import { HttpClientModule } from '@angular/common/http';
import { DataStorageService } from './services/storage.service';
import { BookService } from './services/book.service';
import { UserService } from './services/user.service';
import { ModalService } from './services/modal.service';
@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        AppModuleShared,
        HttpClientModule
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl },
        DataStorageService, BookService, UserService,ModalService
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
