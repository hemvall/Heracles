<template>
  <div style="background-color: #141010;" v-if="isConnected">
    <HeaderPage />
    <div style="padding-left: 5%; padding-right:5%; padding-top: 2%;">
      <RouterView />
    </div>
  </div>
  <div style="background-color: #141010; color:white" v-else>
    <ConnexionPage />
  </div>
</template>

<script lang="js">
import { RouterView } from 'vue-router'
import HeaderPage from './views/HeaderPage/HeaderPage.vue'
import ConnexionPage from './views/ConnexionPage/ConnexionPage.vue'
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
      if(localStorage.getItem('isConnected')){
            this.isConnected = true
        }
        else{
          this.isConnected = false
        }
    },
    data() {
        return {
            isConnected: false,
        };
    },
    methods: {
    },
    components: {HeaderPage, ConnexionPage}
}
);

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
