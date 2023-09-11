<template>
    <div class="headerContainer">
        <div class="header">
            <img src="../../assets/logo.png" width="45px">
            <a href="/" class="logo">Heracles</a>

            <div class="header-right">
                <a class="active" href="#home">Home</a>
                <a href="#contact">Contact</a>
                <a>zzz</a>
                <!-- <a @click="location.reload();">Se déconnecter</a> -->
            </div>
        </div>
        <div class="sidenav">
            <a class="item" href="/"><img src="../../assets/Icon/home.svg"> Home</a>
            <a class="item" href="/exercises"><img src="../../assets/Icon/dumbell.svg"> Exercises</a>
            <a class="item"><img src="../../assets/Icon/goal.svg"> Goals</a>
            <a class="item"><img src="../../assets/Icon/user.svg"> My profile</a>
            <a class="item"><img src="../../assets/Icon/logout.svg" @click="clear"> Logout</a>
        </div>

    </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
    created() {
        this.fetchData();
    },
    data() {
        return {
        };
    },
    methods: {
        fetchData() {
            this.loading = true;

        },
        clear(){
            console.log("aa")
            window.localStorage.clear()
        }
    }
}
);
</script>

<style scoped lang="scss">
@import url('./HeaderPage.scss');

* {
    font-family: Arial, Helvetica, sans-serif;
}
</style>