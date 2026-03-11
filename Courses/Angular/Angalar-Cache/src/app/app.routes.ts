import { Routes } from '@angular/router';
import { Sample } from './sample/sample';
import { TemplateDriven } from './template-driven/template-driven';
import { SignalConcept } from './signal-concept/signal-concept';
import { InjectionTokenConcept } from './injection-token-concept/injection-token-concept';
import { MapConcept } from './map-concept/map-concept';
import { ParentComponent } from './parent-component/parent-component';

export const routes: Routes = [
    {
        path:'',
        component:Sample
    },
    {
        path:'template',
        component:TemplateDriven
    },
    {
        path:'signal',
        component:SignalConcept
    },
    {
        path:'injection',
        loadComponent: () =>import('./injection-token-concept/injection-token-concept')
      .then(c => c.InjectionTokenConcept)

    },
    {
        path:'MapSyntax',
        component: MapConcept,
        pathMatch:'full'
    },
    {
        path:'Parent',
        loadComponent: () =>import('./parent-component/parent-component')
      .then(c => c.ParentComponent)
    },
    {
        path:'host',
        loadComponent:()=>import('./host-element/host-element').then(c=>c.HostElement)
    }
];
