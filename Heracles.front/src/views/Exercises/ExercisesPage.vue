<template>
    <div class="wholePage">
        <div class="container" v-if="addCategoryOpen">
            <div class="form">
                <h2 style="color: white;">{{ $t('exercisesPage.AddCateg') }}</h2>
                <form>
                    <p style="color: white; margin-top: 5%;">{{ $t('form.Label') }}</p>
                    <input v-model="label" type="text" />
                    <button @click="createCategory" class="formConfirm">{{ $t('confirm') }}</button>
                    <button @click="addCategoryOpen = !addCategoryOpen" class="formReturn">{{ $t('close') }}</button>
                </form>
            </div>
        </div>
        <div class="container" v-if="addExerciseOpen">
            <div class="form">
                <h2 style="color: white;">{{ $t('exercisesPage.AddExercise') }}</h2>
                <form>
                    <p style="color: white; margin-top: 5%;">{{ $t('form.Label') }}</p>
                    <input v-model="label" type="text" />
                    <button @click="createExercise()" class="formConfirm">{{ $t('confirm') }}</button>
                    <button @click="addExerciseOpen = !addExerciseOpen" class="formReturn">{{ $t('close') }}</button>
                </form>
            </div>
        </div>
        <div class="wholePage">
            <h1 style="color:white; font-size: 42px;">{{ $t('exercisesPage.MainTitle') }}</h1>
            <a class="button" @click="addCategoryOpen = !addCategoryOpen">{{ $t('exercisesPage.AddCateg') }}</a>
            <a class="button" @click="addExerciseOpen = !addExerciseOpen">{{ $t('exercisesPage.AddExercise') }}</a>
            <div v-if="addCategoryOpen">

            </div>
            <div v-if="typesFromUser.length > 0">
                <div v-for="t in typesFromUser" :key="t.id">
                    <h1 class="typeLabel">{{ t.label }}</h1>
                    <div class="exercisesContainer">
                        <RouterLink :to="`/exercise/detail/${e.id}`" v-for="e in exercisesFromType(t.id)" :key="e.id"
                            class="exerciseBlock">
                            <h2 style="white-space: nowrap; display: flex; justify-content: center; font-size: 20px;">{{
                                e.label
                            }}</h2>
                        </RouterLink>
                        <!-- <a class="exerciseBlock">
                        <h2 style="white-space: nowrap; display: flex; justify-content: center; font-size: 20px;"
                            @click="addExerciseOpen = !addExerciseOpen">Ajouter un
                            exercice</h2>
                    </a> -->
                    </div>

                </div>
            </div>
            <h1 v-else style="color:white;"> Aucune catégorie d'exercice n'a chargé.</h1>
        </div>
        <br /><br />
    </div>
</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
        this.userId = localStorage.getItem('userId')
        console.log(this.userId)
    },
    data() {
        return {
            exercises: [],
            types: [],
            userId: 99,
            label: '',
            editLabel: '',
            addCategoryOpen: false,
            addExerciseOpen: false,
            editCategoryOpen: false,
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
                    label: this.label,
                    userId: this.userId
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
        },
        editCategory(tId) {
            const requestOptions = {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    id: tId,
                    label: this.editLabel
                })
            };
            fetch(`${this.$api}/exerciseType/${tId}`, requestOptions)
                .then(response => {
                    if (response.ok) { alert("La catégorie a bien été modifiée") }
                    else { alert("La catégorie n'a pas été modifiée") }
                    this.fetchData()
                })
                .then(response => response.json())
            // .then(data => (this.postId = data.id));
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
@import url('./ExercisesPage.scss');

template {
    margin: 0 10%;
}

.wholePage {
    margin-left: 11%;
    margin-top: 8%;
}

* {
    font-family: Arial, Helvetica, sans-serif;
}</style>