<template>
        <div class="wholePage">
            <!-- <div class="welcomeContainer">
                <div class="child left">
                    <a class="welcomeTitle">Bienvenue, <br> {{ user.name }}!</a><br>
                    <p><strong style="text-decoration: underline;">Enregistre</strong> tes performances et <strong style="text-decoration: underline;">obtiens un suivi</strong> sur tes performances sportives !</p>
                    <img style="position: absolute; height: 200px; z-index: 0;" src="../../assets/sport.svg" />
                </div>
                <div class="child right">
                    <a>Vous ne disposez pas encore du premium <br> {{ user.name }}!</a>
                    <button class="premiumButton">
                        <img style="height: 30px;" src="../../assets/crown.svg" /> Je passe à premium
                    </button>
                </div>
            </div> -->
            <div class="welcomeContainer">
                <div class="">
                    <h1 class="welcomeTitle">Tu souhaites :</h1>
                </div>
                <div class="">
                    
                </div>
            </div>
            <div class="sectionContainer">
                <div class="">
                    <a>Objectifs blabla</a>
                </div>
                <div class="">
                    
                </div>
            </div>
        </div>
        <br /><br />

</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
        this.idUser = localStorage.getItem('userId')
        console.log(this.userId)
    },
    data() {
        return {
            user: {},
            idUser: 0
        };
    },
    methods: {
        fetchData() {
            this.user = {};
            this.loading = true;

            fetch(`${this.$api}/users/${localStorage.getItem("userId")}`)
                .then(r => r.json())
                .then(json => {
                    this.user = json;
                    this.loading = false;
                    return;
                });
        },
        exercisesFromType(tId) {
            return this.exercises.filter(e => e.typeId == tId && e.userId == localStorage.getItem('userId'))
        }
    },
    computed: {
        typesFromUser() {
            return this.types.filter(e => e.userId == localStorage.getItem('userId'))
        },
    }
}
);
</script>

<style scoped lang="scss">
@import url('./DashBoard.scss');

template {
    margin: 0 10%;
}

.wholePage {
    margin-left: 22%;
    margin-top: 8%;
    color: white;
}

* {
    font-family: Arial, Helvetica, sans-serif;
}</style>