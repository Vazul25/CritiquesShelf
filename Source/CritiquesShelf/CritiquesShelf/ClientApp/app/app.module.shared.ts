import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { MatCommonModule } from '@angular/material';
import { MatGridListModule, MatInputModule, MatFormFieldModule, MatCardModule, MatToolbarModule, MatDialogModule, MatButtonModule, MatTabsModule } from '@angular/material';


import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { BookBrowserComponent } from './components/bookBrowser/bookBrowser.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        BookBrowserComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        MatInputModule,
        MatCardModule,
        HttpModule,
        FormsModule,
        MatTabsModule,
        MatButtonModule,
        NoopAnimationsModule,
        MatGridListModule,
        MatDialogModule,
        MatToolbarModule,
        MatFormFieldModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'browse', component: BookBrowserComponent },
            //{ path: 'bookDetails/:id', component: BookDetailsComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
