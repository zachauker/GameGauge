// Composables
import {createRouter, createWebHistory} from 'vue-router'

const routes = [
  {
    path: '/',
    component: () => import('@/layouts/default/Default.vue'),
    children: [
      {
        path: '',
        name: 'Home',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "home" */ '@/views/Home.vue'),
      },
      {
        path: '/register',
        name: 'Register',
        component: () => import('@/views/Accounts/RegisterView.vue')
      },
      {
        path: '/login',
        name: 'LoginView',
        component: () => import('@/views/Accounts/LoginView.vue')
      },
      {
        path: '/account',
        name: 'AccountView',
        component: () => import('@/views/Accounts/AccountView.vue')
      },
      {
        path: '/gamelists/:id',
        name: 'GameListDetails',
        component: () => import('@/views/GameLists/GameListDetailsView.vue')
      },
      {
        path: '/gamelists/:/add',
        name: 'AddGameListGames',
        component: () => import('@/views/GameLists/AddGameListGamesView.vue')
      },
      {
        path: '/gamelists/create',
        name: 'CreateGameList',
        component: () => import('@/views/GameLists/CreateGameListView.vue')
      },
      {
        path: '/games/:slug',
        name: 'GameDetails',
        component: () => import('@/views/Games/GameDetailsView')
      }
    ],
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
