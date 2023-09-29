<template>
  <div>
    <div style="background-color: #111111;" v-if="isConnected">
      <HeaderPage />
      <div style="padding-left: 5%; padding-right:5%; padding-top: 2%;">
        <RouterView />
      </div>
    </div>
    <div style="background-color: #111111; color:white" v-else>
      <ConnexionPage />
    </div>
  </div>
</template>

<script lang="js">
import { RouterView } from 'vue-router'
import HeaderPage from './views/HeaderPage/HeaderPage.vue'
import ConnexionPage from './views/ConnexionPage/ConnexionPage.vue'
import { defineComponent } from 'vue';

export default defineComponent({
  created() {
    if (localStorage.getItem('isConnected')) {
      this.isConnected = true
    }
    else {
      this.isConnected = false
    }
  },
  data() {
    return {
      isConnected: false,
      theme: "mr"
    };
  },
  methods: {
  },
  components: { HeaderPage, ConnexionPage }
}
);

</script>

<style scoped>
:root {
  --background-color: #fff;
  --text-color: #121416d8;
  --link-color: #543fd7;
}

html[data-theme='light'] {
  --background-color: #fff;
  --text-color: #121416d8;
  --link-color: #543fd7;
}

html[data-theme='dark'] {
  --background-color: #212a2e;
  --text-color: #F7F8F8;
  --link-color: #242741;
}

* {
  font-family: 'montserrat';
}
</style>
