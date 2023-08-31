import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '../views/HomePage/HomePage.vue'
import Exercises from '../views/Exercises/ExercisesPage.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomePage
    },
    {
      path: '/exercises/:typeName',
      name: 'exercises',
      component: Exercises
    },
  ]
})

export default router
