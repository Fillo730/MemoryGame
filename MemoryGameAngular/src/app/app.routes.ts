import { Routes } from '@angular/router';
import { HomePage } from './pages/home/home.page';
import { PlayPage } from './pages/play/play.page';
import { LoginPage } from './pages/login/login.page';
import { SignupPage } from './pages/signup/signup.page';
import { ProfilePage } from './pages/profile/profile.page';
import { authGuard } from './guards/authGuard.Guard';
import { AboutPage } from './pages/about/about.page';
import { DashboardPage } from './pages/dashboard/dashboard.page';

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
            { path: 'classifica', component: DashboardPage },
            { path: '', redirectTo: 'home', pathMatch: 'full' }
        ]
    },

    {
        path: 'en',
        children: [
            { path: 'home', component: HomePage },
            { path: 'play', component: PlayPage },
            { path: 'login', component: LoginPage },
            { path: 'signup', component: SignupPage },
            { path: 'about', component: AboutPage},
            { path: 'profile', component: ProfilePage, canActivate: [authGuard] },
            { path: 'leaderboard', component: DashboardPage },
            { path: '', redirectTo: 'home', pathMatch: 'full' }
        ]
    },

    {
        path: 'fr',
        children: [
            { path: 'accueil', component: HomePage },
            { path: 'jouer', component: PlayPage },
            { path: 'connexion', component: LoginPage },
            { path: 'inscription', component: SignupPage },
            { path: 'a-propos', component: AboutPage},
            { path: 'profil', component: ProfilePage, canActivate: [authGuard] },
            { path: 'classement', component: DashboardPage },
            { path: '', redirectTo: 'accueil', pathMatch: 'full' }
        ]
    },

    {
        path: 'de',
        children: [
            { path: 'home', component: HomePage },
            { path: 'spielen', component: PlayPage },
            { path: 'anmelden', component: LoginPage },
            { path: 'registrieren', component: SignupPage },
            { path: 'info', component: AboutPage},
            { path: 'profil', component: ProfilePage, canActivate: [authGuard] },
            { path: 'rangliste', component: DashboardPage },
            { path: '', redirectTo: 'home', pathMatch: 'full' }
        ]
    }
];