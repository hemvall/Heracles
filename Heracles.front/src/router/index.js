import { createRouter, createWebHistory } from 'vue-router'
import DashBoard from '../views/DashBoard/DashBoard.vue'
import Exercises from '../views/Exercises/ExercisesPage.vue'
import SpecificExercise from '../views/Exercises/SpecificExercise/SpecificExercise.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'dashboard',
      component: DashBoard
    },
    {
      path: '/exercises',
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
