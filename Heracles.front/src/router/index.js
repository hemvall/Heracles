import { createRouter, createWebHistory } from 'vue-router'
import DashBoard from '../views/DashBoard/DashBoard.vue'
import LeaderBoard from '../views/LeaderBoard/LeaderBoard.vue'
import Exercises from '../views/Exercises/ExercisesPage.vue'
import Goals from '../views/Goals/GoalsPage.vue'
import CreateGoal from '../views/Goals/CreateGoal/CreateGoal.vue'
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
      path: '/leaderboard',
      name: 'leaderboard',
      component: LeaderBoard
    },
    {
      path: '/exercises',
      name: 'exercises',
      component: Exercises
    },
    {
      path: '/goals',
      name: 'goals',
      component: Goals
    },
    {
      path: '/exercise/detail/:exerciseId',
      name: 'specificId',
      component: SpecificExercise
    },
    {
      path: '/goal/create',
      name: 'CreateGoal',
      component: CreateGoal
    },
  ]
})

export default router
