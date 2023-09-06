<template>
    <div class="title">
        <h1>Connexion</h1>
    </div>
    <div class="inputs">
        <p>Mail</p>
        <input v-model="mail" type="text" />
        <p>Password</p>
        <input v-model="password" type="password" />
        <button @click="Authentification">Se connecter</button>
    </div>
</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
    },
    data() {
        return {
            mail: '',
            password: '',
        };
    },
    methods: {
        fetchData() {
            this.exercises = [];
            this.loading = true;

            fetch(`${this.$api}/exercises`)
                .then(r => r.json())
                .then(json => {
                    this.exercises = json;
                    this.loading = false;
                    return;
                });
        },
        Authentification() {
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    mail: this.mail,
                    password: this.password,
                })
            };
            fetch(`${this.$api}/Users/Authentification`, requestOptions)
                .then(response => {
                    if (response.ok) {
                        localStorage.setItem('isConnected', true)
                        return response.json()
                    }
                    else { alert("Erreur. L'identifiant ou le mot de passe est incorrect.") }
                    this.fetchData()
                })
                .then(data => {
                    localStorage.setItem('userName', data.name)
                    localStorage.setItem('userMail', data.mail)
                    localStorage.setItem('userId', data.id)
                    location.reload()                    
                })
        }
    }
}
);
</script>

<style scoped lang="scss">
@import url('./ConnexionPage.scss');
</style>