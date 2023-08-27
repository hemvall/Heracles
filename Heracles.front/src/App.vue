<template>
  <div style="background-color: #141010;">
    <HeaderPage />
    <div style="padding-left: 5%; padding-right: 5; padding-top: 2%;">
      <RouterView />
    </div>
  </div>
</template>

<script setup>
import { RouterView } from 'vue-router'
import HeaderPage from './views/HeaderPage/HeaderPage.vue'


</script>

<style scoped>

element.style {
  --mainColor: #141010;
  --secondColor: red;
}

* {
  font-family: 'montserrat';
}
</style>
