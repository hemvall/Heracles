import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '../views/HomePage/HomePage.vue'
import Exercises from '../views/Exercises/ExercisesPage.vue'
import SpecificExercise from '../views/Exercises/SpecificExercise/SpecificExercise.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomePage
    },
    {
      path: '/exercises/:typeId',
      name: 'exercises',
      component: Exercises
    },
    {
      path: '/exercise/detail/:exerciseId',
      name: 'specificId',
      component: SpecificExercise
    },
  ]
})

export default router
