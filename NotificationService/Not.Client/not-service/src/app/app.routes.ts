import { Routes } from '@angular/router';
import { AuthComponent } from './Components/Auth/auth-component/auth-component';
import { NotificationsComponent } from './Components/NotificationInfo/notifications-component/notifications-component';

export const routes: Routes = [
  {
    path: 'auth',
    component: AuthComponent,
  },
  {
    path: 'notifications',
    component: NotificationsComponent,
  },
];

// TODO доабвить guard на роуты
