import Vue from 'vue';
import Router from 'vue-router';
import store from '../store';

Vue.use(Router);

const Home = () => import('../views/Home');
const Projects = () => import('../views/Projects');
const Settings = () => import('../views/Settings');
const Project = () => import('../views/Project');
const ProjectParams = () => import('../views/ProjectParams');
const ProjectObjects = () => import('../views/ProjectObjects');
const ProjectDrillingOrder = () => import('../views/ProjectDrillingOrder');
const ProjectConstructionSchedule = () => import('../views/ProjectConstructionSchedule');
const Login = () => import('../views/Login');

const routes = [
  {
    path: '/',
    component: Home,
    meta: {
      requiresAuth: true,
    },
    children: [
      { path: '', component: Projects, meta: { requiresAuth: true } },
    ],
  },
  {
    path: '/home',
    name: 'Home',
    component: Home,
    meta: {
      requiresAuth: true,
    },
    children: [
      { path: 'settings', component: Settings, meta: { requiresAuth: true } },
      { path: 'projects', component: Projects, meta: { requiresAuth: true } },
      {
        path: 'project',
        component: Project,
        meta: { requiresAuth: true },
        children: [
          { path: 'params', component: ProjectParams, meta: { requiresAuth: true } },
          { path: 'objects', component: ProjectObjects, meta: { requiresAuth: true } },
          { path: 'drillingOrder', component: ProjectDrillingOrder, meta: { requiresAuth: true } },
          {
            path: 'constructionSchedule',
            component: ProjectConstructionSchedule,
            meta: { requiresAuth: true },
          },
        ],
      },
    ],
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
];

const router = new Router({
  mode: 'history',
  routes,
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (store.getters.isLoggedIn) {
      next();
      return;
    }
    next('/login');
  } else {
    next();
  }
});

export default router;
