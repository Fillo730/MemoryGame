import { Routes } from '@angular/router';
import { HomePage } from './pages/home/home.page';
import { PlayPage } from './pages/play/play.page';
import { LoginPage } from './pages/login/login.page';
import { SignupPage } from './pages/signup/signup.page';
import { ProfilePage } from './pages/profile/profile.page';
import { authGuard } from './guards/authGuard.Guard';
import { AboutPage } from './pages/about/about.page';

export const routes: Routes = [
    { path: '', redirectTo: 'it/home', pathMatch: 'full' },
    
    {
        path: 'it',
        children: [
            { path: 'home', component: HomePage },
            { path: 'gioca', component: PlayPage },
            { path: 'accesso', component: LoginPage },
            { path: 'registrazione', component: SignupPage },
            { path: 'profilo', component: ProfilePage, canActivate: [authGuard] },
            { path: 'informazioni', component: AboutPage},
            { path: '', redirectTo: 'home', pathMatch: 'full' }
        ]
    },

    {
        path: 'en',
        children: [
            { path: 'home', component: HomePage },
            { path: 'play', component: PlayPage },
            { path: 'login', component: LoginPage },
            { path: 'sign-up', component: SignupPage },
            { path: 'about', component: AboutPage},
            { path: 'profile', component: ProfilePage, canActivate: [authGuard] },
            { path: '', redirectTo: 'home', pathMatch: 'full' }
        ]
    }
];