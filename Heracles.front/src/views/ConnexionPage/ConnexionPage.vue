<template>
    <div class="titleContainer">
        <h1 class="title" style="display: flex; justify-content: center; font-size: 36px; margin-top: 5%; color:#9f8548">
            Heracles</h1>
            <img class="logo" src="../../assets/logo.png" />
    </div>
    <div class="formsContainer">
        <div class="block">
            <div class="ml-3">
                <div class="title" style="margin-top: 7%;">
                    <h1>Connexion</h1>
                </div>
                <div class="inputs">
                    <p>Mail</p>
                    <input v-model="mail" type="text" />
                    <p>Password</p>
                    <input v-model="password" type="password" /><br />
                    <button @click="Authentification" class="connectButton">Se connecter</button><br /><br />
                    <a class="link" @click="openSignUp = !openSignUp">J'ai oublié mon mot de passe.</a>
                </div>
            </div>
        </div>
        <div class="block">
            <div class="ml-3">

                <div class="inputs">
                    <div class="title">
                        <h1>Inscription</h1>
                    </div>
                    <p>Mail</p>
                    <input v-model="signupMail" type="text" />
                    <p>Prénom</p>
                    <input v-model="name" type="text" />
                    <p>Nom de famille</p>
                    <input v-model="lastName" type="text" />
                    <p>Password</p>
                    <input v-model="signupPassword" type="password" /><br>
                    <button @click="SignUp" class="connectButton">Se connecter</button>
                </div>
            </div>
        </div>

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
            name: '',
            lastName: '',
            mail: '',
            password: '',
            signupPassword: '',
            signupMail: '',
            openSignUp: false
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
        },
        SignUp() {
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    name: this.name,
                    lastName: this.lastName,
                    mail: this.signupMail,
                    password: this.signupPassword,
                })
            };
            fetch(`${this.$api}/Users`, requestOptions)
                .then(response => {
                    if (response.ok) {
                        alert("Votre compte a bien été crée, vous pouvez vous connecter")
                        this.openSignUp = false;
                    }
                    else { alert("Votre compte n'a pas été crée. Votre mot de passe ou mail.") }
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

.ml-3 {
    margin-left: 5%;
}
</style>