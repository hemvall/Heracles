import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

const app = createApp(App)
// dev
app.config.globalProperties.$api = 'https://localhost:7000'
// prod
// app.config.globalProperties.$api = 'https://api.heracle.me'

app.use(router)

app.mount('#app')
