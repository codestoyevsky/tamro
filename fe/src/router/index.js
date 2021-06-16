import Vue from 'vue'
import Router from 'vue-router'

// Containers
const TheContainer = () => import('@/containers/TheContainer')

// Views
const Dashboard = () => import('@/views/Dashboard')

const Users = () => import('@/views/users/List')
const CreateUser = () => import('@/views/users/Create')

Vue.use(Router)

export default new Router({
  mode: 'hash',
  linkActiveClass: 'open active',
  scrollBehavior: () => ({ y: 0 }),
  routes: [
    {
      path: '/',
      redirect: '/dashboard',
      name: 'Home',
      component: TheContainer,
      children: [
        {
          path: 'dashboard',
          name: 'Dashboard',
          component: Dashboard
        },
        {
          path: 'users',
          meta: { label: 'Users' },
          component: {
            render(c) { return c('router-view') }
          },
          children: [
            {
              path: '',
              component: Users
            },
            {
              path: ':id/create',
              meta: { label: 'Create User' },
              name: 'Create',
              component: CreateUser
            }
          ]
        }
      ]
    },
  ]
})
