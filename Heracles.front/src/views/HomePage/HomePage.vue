<template>
    <a class="button" @click="formCategoryOpen = !formCategoryOpen">Add a category</a>
    <div v-if="formCategoryOpen">
        <input v-model="label" type="text" />
        <button @click="createCategory">Valider</button>
        <button @click="formCategoryOpen = !formCategoryOpen">Fermer</button>
    </div>
    <router-link :to="`/exercises/${t.id}`" v-for="t in types" :key="t.id">
        <div class="typeBlock">
            <h1>{{ t.label }}</h1>
            <!-- <div class="gallery">
            <div class="section" v-for="e in exercises" :key="e.id">
                <h1>{{ e.label }}</h1>
                <img src="../../assets/logo.png" />
            </div>
        </div> -->
        </div>
    </router-link>
</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
    },
    data() {
        return {
            exercises: [],
            types: [],
            label: '',
            formCategoryOpen: false,

        };
    },
    methods: {
        fetchData() {
            this.exercises = [];
            this.types = [];
            this.loading = true;

            fetch(`${this.$api}/exercisetype`)
                .then(r => r.json())
                .then(json => {
                    this.types = json;
                    this.loading = false;
                    return;
                });
            fetch(`${this.$api}/exercises`)
                .then(r => r.json())
                .then(json => {
                    this.exercises = json;
                    this.loading = false;
                    return;
                });
        },
        createCategory() {
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    label: this.label
                })
            };
            fetch(`${this.$api}/exerciseType`, requestOptions)
                .then(response => {
                    if (response.ok) { alert("La catégorie a bien été créee") }
                    else { alert("La catégorie n'a pas été créee") }
                    this.fetchData()
                })
                .then(response => response.json())
            // .then(data => (this.postId = data.id));
        }
    }
}
);
</script>

<style scoped lang="scss">
@import url('./HomePage.scss');

template {
    margin: 0 10%;
}
</style>