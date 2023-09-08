import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
// import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'

// import 'bootstrap/dist/css/bootstrap.css'
// import 'bootstrap-vue/dist/bootstrap-vue.css'

const app = createApp(App)
// dev
app.config.globalProperties.$api = 'https://localhost:7000'
// prod
// app.config.globalProperties.$api = 'https://api.heracle.me'

app.use(router)
// app.use(BootstrapVue)
// app.use(IconsPlugin)

app.mount('#app')
