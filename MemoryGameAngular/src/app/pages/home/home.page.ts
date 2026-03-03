//Angular
import { Component, inject } from '@angular/core';

//Components
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { CustomButtonComponent } from '../../components/custom-button/custom-button.component';
import { NatureGalleryComponent } from '../../components/nature-gallery/nature-gallery.component';

//Services
import { DifficultiesService } from '../../services/difficulties-service.service';

//public
import { defaultImages } from '../../../../public/DefaultImages';

//i18n
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'home-page',
  imports: [HeaderComponent, FooterComponent, CustomButtonComponent, NatureGalleryComponent, TranslateModule],
  templateUrl: './home.page.html',
  styleUrl: './home.page.css',
})

export class HomePage {
  public defaultImages: string[] = defaultImages;
}
