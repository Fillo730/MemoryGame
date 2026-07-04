//Angular
import { Component } from '@angular/core';

//components
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { GithubButtonComponent } from '../../components/github-button/github-button.component';
import { LinkedinButtonComponent } from '../../components/linkedin-button/linkedin-button.component';

//i18n
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'about-page',
  imports: [TranslatePipe, GithubButtonComponent,LinkedinButtonComponent,HeaderComponent,FooterComponent],
  templateUrl: './about.page.html',
  styleUrl: './about.page.css',
})
export class AboutPage {

}
